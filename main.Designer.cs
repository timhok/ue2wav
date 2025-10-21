namespace ue2wav
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputBrowseButton = new System.Windows.Forms.Button();
            this.InputFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.outputBrowseButton = new System.Windows.Forms.Button();
            this.OutputFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.inputFolderTextBox = new System.Windows.Forms.TextBox();
            this.outputFolderTextBox = new System.Windows.Forms.TextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.conversionProgressBar = new System.Windows.Forms.ProgressBar();
            this.FilesDataGrid = new System.Windows.Forms.DataGridView();
            this.inputLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uexp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.FilesDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // inputBrowseButton
            // 
            this.inputBrowseButton.Location = new System.Drawing.Point(646, 26);
            this.inputBrowseButton.Name = "inputBrowseButton";
            this.inputBrowseButton.Size = new System.Drawing.Size(142, 27);
            this.inputBrowseButton.TabIndex = 2;
            this.inputBrowseButton.Text = "Browse…";
            this.inputBrowseButton.UseVisualStyleBackColor = true;
            this.inputBrowseButton.Click += new System.EventHandler(this.InputBrowseButton_Click);
            // 
            // outputBrowseButton
            // 
            this.outputBrowseButton.Location = new System.Drawing.Point(646, 77);
            this.outputBrowseButton.Name = "outputBrowseButton";
            this.outputBrowseButton.Size = new System.Drawing.Size(142, 27);
            this.outputBrowseButton.TabIndex = 5;
            this.outputBrowseButton.Text = "Browse…";
            this.outputBrowseButton.UseVisualStyleBackColor = true;
            this.outputBrowseButton.Click += new System.EventHandler(this.OutputBrowseButton_Click);
            // 
            // inputFolderTextBox
            // 
            this.inputFolderTextBox.Location = new System.Drawing.Point(15, 29);
            this.inputFolderTextBox.Name = "inputFolderTextBox";
            this.inputFolderTextBox.ReadOnly = true;
            this.inputFolderTextBox.Size = new System.Drawing.Size(625, 20);
            this.inputFolderTextBox.TabIndex = 1;
            this.inputFolderTextBox.TabStop = false;
            // 
            // outputFolderTextBox
            // 
            this.outputFolderTextBox.Location = new System.Drawing.Point(15, 80);
            this.outputFolderTextBox.Name = "outputFolderTextBox";
            this.outputFolderTextBox.ReadOnly = true;
            this.outputFolderTextBox.Size = new System.Drawing.Size(625, 20);
            this.outputFolderTextBox.TabIndex = 4;
            this.outputFolderTextBox.TabStop = false;
            // 
            // convertButton
            // 
            this.convertButton.Enabled = false;
            this.convertButton.Location = new System.Drawing.Point(15, 118);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(138, 27);
            this.convertButton.TabIndex = 6;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // conversionProgressBar
            // 
            this.conversionProgressBar.Location = new System.Drawing.Point(170, 118);
            this.conversionProgressBar.Name = "conversionProgressBar";
            this.conversionProgressBar.Size = new System.Drawing.Size(618, 27);
            this.conversionProgressBar.TabIndex = 7;
            // 
            // FilesDataGrid
            // 
            this.FilesDataGrid.AllowUserToAddRows = false;
            this.FilesDataGrid.AllowUserToDeleteRows = false;
            this.FilesDataGrid.AllowUserToResizeColumns = false;
            this.FilesDataGrid.AllowUserToResizeRows = false;
            this.FilesDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilesDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.FilesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FilesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.uexp,
            this.status});
            this.FilesDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.FilesDataGrid.Location = new System.Drawing.Point(15, 161);
            this.FilesDataGrid.MultiSelect = false;
            this.FilesDataGrid.Name = "FilesDataGrid";
            this.FilesDataGrid.ReadOnly = true;
            this.FilesDataGrid.RowHeadersVisible = false;
            this.FilesDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FilesDataGrid.ShowCellErrors = false;
            this.FilesDataGrid.ShowCellToolTips = false;
            this.FilesDataGrid.ShowEditingIcon = false;
            this.FilesDataGrid.ShowRowErrors = false;
            this.FilesDataGrid.Size = new System.Drawing.Size(773, 277);
            this.FilesDataGrid.TabIndex = 8;
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(12, 13);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(77, 13);
            this.inputLabel.TabIndex = 0;
            this.inputLabel.Text = "Input directory:";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(12, 64);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(85, 13);
            this.outputLabel.TabIndex = 3;
            this.outputLabel.Text = "Output directory:";
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FileName.HeaderText = "File";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 48;
            // 
            // uexp
            // 
            this.uexp.HeaderText = ".uexp found";
            this.uexp.Name = "uexp";
            this.uexp.ReadOnly = true;
            this.uexp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.uexp.Width = 70;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 450);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.FilesDataGrid);
            this.Controls.Add(this.conversionProgressBar);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.outputFolderTextBox);
            this.Controls.Add(this.outputBrowseButton);
            this.Controls.Add(this.inputFolderTextBox);
            this.Controls.Add(this.inputBrowseButton);
            this.Name = "main";
            this.Text = "ue2wav";
            ((System.ComponentModel.ISupportInitialize)(this.FilesDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button inputBrowseButton;
        private System.Windows.Forms.Button outputBrowseButton;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.TextBox inputFolderTextBox;
        private System.Windows.Forms.TextBox outputFolderTextBox;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.ProgressBar conversionProgressBar;
        private System.Windows.Forms.FolderBrowserDialog InputFolder;
        private System.Windows.Forms.FolderBrowserDialog OutputFolder;
        private System.Windows.Forms.DataGridView FilesDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn uexp;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}
