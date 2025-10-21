
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ue2wav
{
    public partial class main : Form
    {
        private readonly List<FileConversionItem> _items = new List<FileConversionItem>();
        private static readonly Regex DurationRegex = new Regex(@"Duration:\s*(?<value>\d+(\.\d+)?)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());
        }

        public main()
        {
            InitializeComponent();
        }

        private void InputBrowseButton_Click(object sender, EventArgs e)
        {
            if (InputFolder.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(InputFolder.SelectedPath))
            {
                return;
            }

            inputFolderTextBox.Text = InputFolder.SelectedPath;
            RefreshFileList();
        }

        private void OutputBrowseButton_Click(object sender, EventArgs e)
        {
            if (OutputFolder.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(OutputFolder.SelectedPath))
            {
                return;
            }

            outputFolderTextBox.Text = OutputFolder.SelectedPath;
            UpdateConvertButtonState();
        }

        private async void ConvertButton_Click(object sender, EventArgs e)
        {
            convertButton.Enabled = false;
            FilesDataGrid.Enabled = false;
            conversionProgressBar.Value = 0;

            var convertibleItems = _items.Where(item => item.HasUexp).ToList();
            conversionProgressBar.Maximum = convertibleItems.Count == 0 ? 1 : convertibleItems.Count;

            foreach (var item in convertibleItems)
            {
                UpdateRowStatus(item, "Preparing…");
                try
                {
                    string tempFile = await BuildTemporaryFileAsync(item);
                    try
                    {
                        string outputPath = item.GetOutputPath(outputFolderTextBox.Text);
                        Directory.CreateDirectory(Path.GetDirectoryName(outputPath) ?? outputFolderTextBox.Text);
                        string durationText = await RunBinkadecAsync(tempFile, outputPath);
                        UpdateRowStatus(item, string.IsNullOrEmpty(durationText) ? "Completed" : $"Completed ({durationText})");
                    }
                    finally
                    {
                        SafeDelete(tempFile);
                    }
                }
                catch (Exception ex)
                {
                    UpdateRowStatus(item, $"Failed: {ex.Message}");
                }

                conversionProgressBar.Value = Math.Min(conversionProgressBar.Value + 1, conversionProgressBar.Maximum);
            }

            FilesDataGrid.Enabled = true;
            UpdateConvertButtonState();
        }

        private void RefreshFileList()
        {
            FilesDataGrid.Rows.Clear();
            _items.Clear();

            string inputPath = inputFolderTextBox.Text;
            if (string.IsNullOrWhiteSpace(inputPath) || !Directory.Exists(inputPath))
            {
                UpdateConvertButtonState();
                return;
            }

            foreach (string ubulkPath in Directory.GetFiles(inputPath, "*.ubulk", SearchOption.TopDirectoryOnly))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(ubulkPath);
                string uexpPath = Path.Combine(inputPath, $"{fileNameWithoutExtension}.uexp");
                bool hasUexp = File.Exists(uexpPath);
                int rowIndex = FilesDataGrid.Rows.Add(Path.GetFileName(ubulkPath), hasUexp, hasUexp ? "Ready" : "Missing .uexp");
                var row = FilesDataGrid.Rows[rowIndex];
                var item = new FileConversionItem(ubulkPath, uexpPath, row, hasUexp);
                row.Tag = item;
                _items.Add(item);
            }

            UpdateConvertButtonState();
        }

        private void UpdateConvertButtonState()
        {
            bool hasInput = !string.IsNullOrWhiteSpace(inputFolderTextBox.Text) && Directory.Exists(inputFolderTextBox.Text);
            bool hasOutput = !string.IsNullOrWhiteSpace(outputFolderTextBox.Text) && Directory.Exists(outputFolderTextBox.Text);
            bool hasConvertible = _items.Any(item => item.HasUexp);
            convertButton.Enabled = hasInput && hasOutput && hasConvertible && FilesDataGrid.Enabled;
        }

        private async Task<string> BuildTemporaryFileAsync(FileConversionItem item)
        {
            byte[] header = ExtractHeader(item.UexpPath);
            if (header.Length == 0)
            {
                throw new InvalidOperationException("Header not found in .uexp");
            }

            string tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.tmp");
            using (var tempStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await tempStream.WriteAsync(header, 0, header.Length).ConfigureAwait(false);
                using (var ubulkStream = new FileStream(item.UbulkPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await ubulkStream.CopyToAsync(tempStream).ConfigureAwait(false);
                }
            }

            return tempFile;
        }

        private static byte[] ExtractHeader(string uexpPath)
        {
            if (!File.Exists(uexpPath))
            {
                return Array.Empty<byte>();
            }

            byte[] fileBytes = File.ReadAllBytes(uexpPath);
            byte[] marker = { (byte)'A', (byte)'B', (byte)'E', (byte)'U' };

            for (int i = 0; i <= fileBytes.Length - marker.Length - 24; i++)
            {
                if (MatchesMarker(fileBytes, marker, i))
                {
                    byte[] header = new byte[marker.Length + 24];
                    Buffer.BlockCopy(fileBytes, i, header, 0, header.Length);
                    return header;
                }
            }

            return Array.Empty<byte>();
        }

        private static bool MatchesMarker(byte[] source, byte[] marker, int index)
        {
            for (int i = 0; i < marker.Length; i++)
            {
                if (source[index + i] != marker[i])
                {
                    return false;
                }
            }
            return true;
        }

        private async Task<string> RunBinkadecAsync(string tempFile, string outputFile)
        {

            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "binkadec.exe");
            if (!File.Exists(exePath))
            {
                throw new FileNotFoundException("binkadec.exe not found", exePath);
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"-i \"{tempFile}\" -o \"{outputFile}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            using (var process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string stdOut = await process.StandardOutput.ReadToEndAsync().ConfigureAwait(false);
                string stdErr = await process.StandardError.ReadToEndAsync().ConfigureAwait(false);
                await Task.Run(() => process.WaitForExit()).ConfigureAwait(false);

                if (!string.IsNullOrWhiteSpace(stdOut))
                {
                    Console.WriteLine(stdOut);
                }

                if (!File.Exists(outputFile))
                {
                    string message = string.Join(" ", new[] { stdErr.Trim(), stdOut.Trim() }.Where(s => !string.IsNullOrEmpty(s)));
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        message = process.ExitCode == 0
                            ? "Conversion failed and output file was not produced."
                            : $"binkadec exited with code {process.ExitCode}";
                    }
                    throw new InvalidOperationException(message);
                }
                return ExtractDurationText(stdOut);
            }
        }

        private static string ExtractDurationText(string output)
        {
            if (string.IsNullOrWhiteSpace(output))
            {
                return string.Empty;
            }

            var match = DurationRegex.Match(output);
            if (!match.Success)
            {
                return string.Empty;
            }

            if (!double.TryParse(match.Groups["value"].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double seconds))
            {
                return string.Empty;
            }

            return FormatDuration(seconds);
        }

        private static string FormatDuration(double seconds)
        {
            if (double.IsNaN(seconds) || double.IsInfinity(seconds) || seconds < 0)
            {
                return string.Empty;
            }

            int totalSeconds = (int)Math.Floor(seconds);
            int minutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;

            return $"{minutes}:{remainingSeconds:00}";
        }

        private static void SafeDelete(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void UpdateRowStatus(FileConversionItem item, string status)
        {
            if (item.Row.IsNewRow)
            {
                return;
            }

            item.Row.Cells[2].Value = status;
        }

        private class FileConversionItem
        {
            public FileConversionItem(string ubulkPath, string uexpPath, DataGridViewRow row, bool hasUexp)
            {
                UbulkPath = ubulkPath;
                UexpPath = uexpPath;
                Row = row;
                HasUexp = hasUexp;
            }

            public string UbulkPath { get; }
            public string UexpPath { get; }
            public DataGridViewRow Row { get; }
            public bool HasUexp { get; }

            public string GetOutputPath(string outputDirectory)
            {
                string fileName = Path.GetFileNameWithoutExtension(UbulkPath) ?? "output";
                return Path.Combine(outputDirectory, $"{fileName}.wav");
            }
        }
    }
}
