# Installation for Unix-like OSes

Unlike most projects out there written for Unix-like OSes, `KSPNameGen` comes with a `Makefile`, but no `configure` script. One must, however, ensure that the core dependency, Mono, is installed, or the `make` process will fail.

Mono can be obtained here: <<http://www.mono-project.com/download/>>

## Setting up

First, clone the repository from GitHub:

```plaintext
git clone https://github.com/KerbaeAdAstra/KSPNameGen.git
cd KSPNameGen
```

If you wish to attempt to build directly from the `develop` branch (that is, potentially unstable and under active development), do not do anything here. If you're feeling less ambitious, run the following command to obtain the source code from a specific version:

`git checkout <tag>`

If there is a specific commit you wish to build from, that can be checked out too:

`git checkout <hash>`

Alternatively, if you just want to obtain the latest source code package, download it from the GitHub releases page. Note that old releases are purged, so only the latest version may be obtained through this route.

## Running `make`

Once all of that is complete, simply run `make`. `make` will generate messages as it compiles KSPNameGen. If it fails during a certain step, there are several possible reasons why it failed. Some of the more common ones follow:

* Problem: `command not found` or similar.
* Solution: Mono is either not installed or not configured correctly. Attempt a reinstall.
* Problem: Compiler error.
* Solution: The code in that commit (if `git checkout <hash>` was executed prior to running `make`) is broken. Try checking out the latest stable code, or another commit.

If `make` succeeded, there should be an executable `KSPNameGen.exe` in the current directory. Congratulations!

## Where to from here

As it is, the executable can be run from the current directory by executing `mono KSPNameGen.exe`.

However, if one wished to be able to run if without invoking `mono`, the next step would be to run `make install`, which will copy a script and the executable under the `/usr/local` tree. As this involves `root`-owned directories, it must be run with root privileges or sudo.

After running `make install`, KSPNameGen may be run by executing `kspng`. If it returns `command not found` or similar, try invoking it directly via `/usr/local/bin/kspng`.

If at any time you wish to uninstall KSPNameGen, simply run `make uninstall`. This command also requires root privileges or sudo.

If you only wish to delete the compiled executable in the current directory (for instance, after a `make install`), run `make clean`.