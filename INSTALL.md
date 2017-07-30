# Installation for Unix-like OSes

Unlike most projects out there written for Unix-like OSes, `KSPNameGen` comes with a `Makefile`, but no `configure` script. One must, however, ensure that the core dependency, Mono, is installed, or the `make` process will fail.

## Obtaining Mono

### Major Linux distributions

Debian (and derivatives):
```
# apt-get update
# apt-get install mono-devel
```

RHEL (and derivatives, but **NOT** Fedora 22 and later):

```
# yum update
# yum install mono-devel
```

Fedora 22 and later:

```
# dnf update
# dnf install mono-devel
```

Note that if you are running those commands as a non-root user, simply prepend `sudo` to the commands.

### FreeBSD and derivatives

FreeBSD (pkg):

```
# pkg update
# pkg install mono
```

FreeBSD (ports):

```
# cd /usr/ports/lang/mono
# make install clean
```

Note that if you are running those commands as a non-root user, and the system does not have the utility `sudo` installed, run this command:
```
% su
Password:
#
```
You must be part of the system's `wheel` group to do this, however.

## Setting up

First, clone the repository from GitHub:

```
$ git clone https://github.com/KerbaeAdAstra/KSPNameGen.git
$ cd KSPNameGen
```

If you wish to attempt to build directly from the `develop` branch (that is, potentially unstable and under active development), do not do anything here. If you're feeling less ambitious, run the following command to obtain the latest stable source code (v0.2.1 at the time of this writing) instead:

`$ git checkout v0.2.1`

If there is a specific commit you wish to build from, that can be checked out too:

`$ git checkout da39a3ee5e6b4b0d3255bfef95601890afd80709`

Alternatively, if you just want to obtain the latest source code package, download it from the GitHub releases page. Note that old releases are purged, so only the latest version may be obtained through this route.

## Running `make`

Once all of that is complete, simply run `make` like so:

`$ make`

`make` will generate a lot of messages as it compiles KSPNameGen. If it fails during a certain step, there are several possible reasons why it failed. Some of the more common ones follow:

* Problem: `command not found` or similar.
* Solution: Mono is either not installed or not configured correctly. Attempt a reinstall.
* Problem: Compiler error.
* Solution: The code in that commit (if `git checkout <hash>` was executed prior to running `make`) is broken. Try checking out the latest stable code, or another commit.

If `make` succeeded, there should be an executable `KSPNameGen.exe` in the current directory. Congratulations!

## Where to from here

The next step would be to run `make install`, which will copy a script and the executable under the `/usr` tree. As this involves `root`-owned directories, it must be run with root privileges or sudo.

If at any time you wish to uninstall KSPNameGen, simply run `make uninstall`. This command also requires root privileges or sudo.

If you only wish to delete the compiled executable in the current directory (for instance, after a `make install`), run `make clean`.