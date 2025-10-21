# ue2wav

A Windows GUI utility for converting Unreal Engine game audio files (`.ubulk` + `.uexp`) exported via [Fmodel](https://github.com/4sval/FModel) into standard WAV files using [binkadec](https://github.com/Keisawaakira/BinkadecWithWavHeader), without building "Local Mapping File" (usmap).

Tested with UE 5.6 games using Fmodel, useful for gathering background music.

**Please respect game developers and use it on games that you own; support creators by buying OST packs if they are available.**

## Requirements
- Windows 10 or later
- Net Framework 4.8
- `.ubulk` and matching `.uexp` files exported from Fmodel using "`Export Folder's Packages Raw Data (.uasset)`"
- [binkadec](https://github.com/Keisawaakira/BinkadecWithWavHeader) binary that handles actual conversion from Bink Audio to WAV

## Getting Started
- Download the latest version from Releases and unpack it.
- Make sure `binkadec.exe` is placed in the same directory as `ue2wav.exe` (download and unpack from [BinkadecWithWavHeader releases](https://github.com/Keisawaakira/BinkadecWithWavHeader/releases/tag/1.0.2)) or use the bundled release.

## Usage
1. Select the **Input directory** that contains your `.ubulk` files. The app automatically checks for `.uexp` files and lists every detected pair.
2. Choose the **Output directory** where the resulting WAV files should be saved.
3. Press **Convert**. ue2wav combines header data from `.uexp` files and audio data from `.ubulk` files before running `binkadec.exe`. The table updates the status for each file and shows the duration when it is reported by `binkadec.exe`.
4. When the progress bar fills, conversion is done. WAV files should appear in the output directory using their original base filenames.

## Troubleshooting
- The **Convert** button stays disabled if no valid `.ubulk` / `.uexp` pairs are found.
- If a file fails to convert, review the status for error details. Missing headers or a missing `binkadec.exe` are common causes, you may also try tinkering with Fmodel before exporting files.

## Disclaimer
Unreal Engine and associated trademarks are owned by Epic Games. This project is an independent tool provided without affiliation, endorsement, or warranty, and you assume all responsibility for its use; I carry no liability for any misuse or damages.
