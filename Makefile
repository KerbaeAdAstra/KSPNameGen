# Makefile
#
# This file is part of KSPNameGen, a free (gratis and libre) name generator for Kerbal Space Program.
# Kerbal Space Program is (c) 2011-2017 Squad. All Rights Reserved.
# KSPNameGen is (c) 2016-2017 the Kerbae ad Astra group <kerbaeadastra@gmail.com>.
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.

# Command definitions

cp := $(shell which cp)
rm := $(shell which rm)
mkdir := $(shell which mkdir)
platform := $(shell uname -s)

# Flag definitions

cpflags = -f
rmflags = -rf
buildflags = /verbosity:diagnostic
mkdirflags = -p
bundleflags = --cross default

# Path/File definitions

localbin = /usr/local/bin
basename = KSPNameGen
executable = kspng

# Test for msbuild/xbuild
buildtest := $(shell which msbuild 2> /dev/null; echo $$?)
ifeq ($(buildtest),1)
    buildtest := $(shell which xbuild 2> /dev/null; echo $$?)
    ifeq ($(buildtest),1)
        $(error Suitable build tools were not located in your PATH. Please check your build environment)
    else
        build := $(shell which xbuild)
    endif
else
    build := $(shell which msbuild)
endif
buildtest =

# Test for mkbundle
bundletest := $(shell which mkbundle 2> /dev/null; echo $$?)
ifeq ($(buildtest),1)
	$(error Suitable build tools were not located in your PATH. Please check your build environment)
else
    bundle := $(shell which mkbundle)
endif
bundletest =

all: $(sln)
	$(build) $(basename).sln
	$(bundle) -v -o $(executable) $(basename).exe $(bundleflags)

.PHONY: clean
clean:
	$(rm) $(rmflags) $(executable)
	$(rm) $(rmflags) $(basename).exe
	$(rm) $(rmflags) $(basename).pdb
	$(rm) $(rmflags) $(basename)/obj

install: all
	$(mkdir) $(mkdirflags) $(localbin)
	$(cp) $(cpflags) $(executable $(localbin)

.PHONY: uninstall
uninstall:
	$(rm) $(rmflags) $(localbin)/$(executable)
