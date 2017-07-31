# KSPNameGen

[![GitHub (pre-)release](https://img.shields.io/github/release/KerbaeAdAstra/KSPNameGen/all.svg)](https://github.com/KerbaeAdAstra/KSPNameGen/releases)
[![License](https://img.shields.io/github/license/KerbaeAdAstra/KSPNameGen.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://travis-ci.org/KerbaeAdAstra/KSPNameGen.svg?branch=develop)](https://travis-ci.org/KerbaeAdAstra/KSPNameGen)

---

```plaintext
 /##   /##  /######  /#######  /##   /##                                    /######
| ##  /##/ /##__  ##| ##__  ##| ### | ##                                   /##__  ##
| ## /##/ | ##  \__/| ##  \ ##| ####| ##  /######  /######/####   /###### | ##  \__/  /######  /#######
| #####/  |  ###### | #######/| ## ## ## |____  ##| ##_  ##_  ## /##__  ##| ## /#### /##__  ##| ##__  ##
| ##  ##   \____  ##| ##____/ | ##  ####  /#######| ## \ ## \ ##| ########| ##|_  ##| ########| ##  \ ##
| ##\  ##  /##  \ ##| ##      | ##\  ### /##__  ##| ## | ## | ##| ##_____/| ##  \ ##| ##_____/| ##  | ##
| ## \  ##|  ######/| ##      | ## \  ##|  #######| ## | ## | ##|  #######|  ######/|  #######| ##  | ##
|__/  \__/ \______/ |__/      |__/  \__/ \_______/|__/ |__/ |__/ \_______/ \______/  \_______/|__/  |__/
```

## What is KSPNameGen?

KSPNameGen is a free (both gratis and libre) random name generator for the
popular PC game Kerbal Space Program. Now rewritten in C#!

## Command-Line Arguments

KSPNameGen now comes with a non-interactive mode, for when you just want a
couple of names and don't want to mess around with the interactive interface.

```
Usage: KSPNameGen.exe [-i|--interactive] [-n|--non-interactive parameter number [buffsize]] [-h|--help]
-i, --interactive: interactive mode (default option if no parameter specified)
-n, --non-interactive: non-interactive mode
-h, --help: show this help
parameter: either of [f|s] [r|c|p] [m|f] in this order. Run in interactive mode to learn more.
number: how many names to generate at once.
buffsize: the size of the buffer (i.e. how many names to write to stdout at once).
number and buffsize must be positive nonzero integers less than 18,446,744,073,709,551,615 (2^64-1).
`buffsize' is optional; if not given, the default is 48.
`parameter', `number', and `buffsize' are only used with non-interactive mode.
```

## License

See LICENSE.md.

## Contact Us

If you're feeling nostalgic, don't hesitate to hit us up on IRC. We have our
own channel (#kerbaeadastra @ irc.freenode.net).
