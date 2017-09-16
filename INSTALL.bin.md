# Installation of binary on all platforms

## Introduction

If you're on a Unix-like operating system, and feel like compiling from source is beyond your knowledge, then fret not - for there are precompiled executables available.

Note that this is the only way to obtain KSPNameGen on Microsoft Windows.

## Downloading and Verifying

The downloads are offered [here](https://github.com/KerbaeAdAstra/KSPNameGen/releases), and come in both zip archives (mainly geared towards Microsoft Windows users) and XZ-compressed tarballs (mainly geared towards users of Unix-like operating systems). But there's nothing that's barring a Windows user from downloading a XZ-compressed tarball, as long as they have the means to extract it.

Once the package is downloaded, extract it. There should be two files, `KSPNameGen-vx.x.x.exe` and `KSPNameGen-vx.x.x.exe.asc`. If you have GPG tools installed, now is the time to verify the executable (with the .asc, which is the signature file). While not strictly required, it's definitely recommended if you're paranoid. Unix users may consult `INSTALL.src.md` on how to verify it with command-line tools.

If you do choose to verify it, and it returns a bad signature, then dispose of the package and its contents; there is no guarantee that the executable is what it says it is.

Once you've downloaded it (and verified it, if you so chose), it is time to proceed to the next step.

## Installation

KSPNameGen is a standalone executable, and doesn't have an installer. All you need to do is to install the .NET Framework (Microsoft Windows) or Mono (Unix-like operating systems).

On Windows, as it is a .exe, you can double-click it to run it (provided .NET is installed). On Unix-like operating systems, it's more complicated, as they are not native executables.

In a Terminal window, run the following command: `mono KSPNameGen-vx.x.x.exe`. This will start KSPNameGen. Append flags to it if you wish to use it in non-interactive mode or other things.
