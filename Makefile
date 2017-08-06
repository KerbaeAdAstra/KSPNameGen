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

cprmflags = -f
mkdirflags = -p

# Path definitions

bin = KSPNameGen.exe
sln = KSPNameGen.sln
pdb = KSPNameGen.pdb
script = kspng
libexec = /usr/local/libexec
localbin = /usr/local/bin

# Test for msbuild/xbuild
buildtest := $(shell which msbuild 2> /dev/null; echo $$?)
ifeq ($(buildtest),1)
	buildtest := $(shell which xbuild 2> /dev/null; echo $$?)
	ifeq ($(buildtest),1)
		$(error Neither msbuild nor xbuild was located! Please check your build environment.)
	else
		build := $(shell which xbuild)
	endif
else
	build := $(shell which msbuild)
endif

all: $(sln)
	$(build) $(sln)

.PHONY: clean
clean:
	$(rm) $(cprmflags) $(bin)
	$(rm) $(cprmflags) $(pdb)

install: all $(projdir)/$(script)
	$(mkdir) $(mkdirflags) $(libexec) $(localbin)
	$(cp) $(cprmflags) $(bin) $(libexec)
	$(cp) $(cprmflags) $(projdir)/$(script) $(localbin)

.PHONY: uninstall
uninstall:
	$(rm) $(cprmflags) $(libexec)/$(bin)
	$(rm) $(cprmflags) $(localbin)/$(script)
