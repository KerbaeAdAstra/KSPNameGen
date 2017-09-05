#

```plaintext
    __ _______ ____  _   __                     ______
   / //_/ ___// __ \/ | / /___ _____ ___  ___  / ____/__  ____
  / ,<  \__ \/ /_/ /  |/ / __ `/ __ `__ \/ _ \/ / __/ _ \/ __ \
 / /| |___/ / ____/ /|  / /_/ / / / / / /  __/ /_/ /  __/ / / /
/_/ |_/____/_/   /_/ |_/\__,_/_/ /_/ /_/\___/\____/\___/_/ /_/
```

[![GitHub (pre-)release](https://img.shields.io/github/release/KerbaeAdAstra/KSPNameGen/all.svg)](https://github.com/KerbaeAdAstra/KSPNameGen/releases)
[![License](https://img.shields.io/github/license/KerbaeAdAstra/KSPNameGen.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://travis-ci.org/KerbaeAdAstra/KSPNameGen.svg?branch=develop)](https://travis-ci.org/KerbaeAdAstra/KSPNameGen)

---

## What is KSPNameGen? ##

KSPNameGen is a free (both gratis and libre) random name generator for the
popular PC game Kerbal Space Program. Now rewritten in C#!

## Command-Line Arguments ##

KSPNameGen now comes with a non-interactive mode, for when you just want a
couple of names and don't want to mess around with the interactive interface.

```plaintext
Usage: KSPNameGen.exe [flags] [args]
A list of valid flags and their arguments follow.
-h --help:        No argument. Displays this message.
-t --type:        A string indicating the type of name to generate. Defaults to fpm.
-b --buffer:      An integer indicating the number of names to write to stdout per frame.
-f --file:        A string indicating the output file, using either relative or absolute paths.
-i --interactive: No argument. Forces interactive mode; default.
-n --number:      An integer indicating the number of names to generate. Also noninteractive.
All other (invalid) flags and arguments will result in this message being shown.
```

## License ##

See LICENSE.md.

## Contact Us ##

Please don't hesitate to hit us up on IRC. We have our own channel (#kerbaeadastra @ irc.freenode.net).
