# Installation from source on Unix-like operating systems

## Introduction

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

## Verifying the package

It's always a good idea to verify the integrity of the source package after you've downloaded it. While not strictly required, if you're paranoid it's best to do so.

First, obtain GPG through your operating system's package manager (or similar). Once you have that installed, obtain the signature for the *correct package* on the downloads page (which should have a file extension `.asc`). If you download the wrong signature for the package, it will always report a bad signature, no matter what. Be forewarned.

Once you have both the source package and signature for that package downloaded, run this command:

`gpg --verify /path/to/signature.asc /path/to/src/package.tar.xz`

It should output a message similar to this:

```plaintext
gpg: Signature made [...]
gpg:                using RSA key [...]
gpg: Good signature from "[...]" [ultimate]
```

If the last line looks like this instead:

```plaintext
gpg: Signature made [...]
gpg:                using RSA key [...]
gpg: BAD signature from "[...]" [ultimate]
```

Then the source package has been tampered with or is corrupt. Dispose of it and try redownloading it.

If the source package returns a good signature, go ahead and unpack it:

```plaintext
tar xJvf KSPNameGen-vx.x.x-src.tar.xz
cd KSPNameGen-vx.x.x-src
```

## Running `make`

Once all of that is complete, simply run `make`. `make` will generate messages as it compiles KSPNameGen. If it fails during a certain step, there are several possible reasons why it failed. Some of the more common ones follow:

* Problem: `Makefile:64: *** Suitable build tools were not located in your PATH. Please check your build environment.  Stop.`
* Solution: `make` did not detect any build tools in your PATH. Try manually providing the PATH to it (`PATH=/path/to/mono/:$PATH make`) if it is installed, or (re)install it if it is not installed or broken.
* Problem: `Build FAILED.`
* Solution: The code in that commit (if `git checkout <hash>` was executed prior to running `make`) is broken. Try checking out the latest stable code, or another commit.

If `make` succeeded, there should be an executable `KSPNameGen.exe` in the current directory. Congratulations!

## Where to from here

As it is, the executable can be run from the current directory by executing `mono KSPNameGen.exe`.

However, if one wished to be able to run if without invoking `mono`, the next step would be to run `make install`, which will copy a script and the executable under the `/usr/local` tree. As this involves `root`-owned directories, it must be run with root privileges or sudo.

After running `make install`, KSPNameGen may be run by executing `kspng`. If it returns `command not found` or similar, try invoking it directly via `/usr/local/bin/kspng`.

If at any time you wish to uninstall KSPNameGen, simply run `make uninstall`. This command also requires root privileges or sudo.

If you only wish to delete the compiled executable in the current directory (for instance, after a `make install`), run `make clean`.
