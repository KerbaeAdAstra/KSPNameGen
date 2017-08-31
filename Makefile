# Makefile
#
# This file is part of KSPNameGen, a free (gratis and libre) name generator for
# Kerbal Space Program.
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

# Path/File definitions

script = kspng
libexec = /usr/local/libexec
localbin = /usr/local/bin
basename = KSPNameGen
buildcache = KSPNameGen/obj

# Get the git versioning

stable = 0.3.0
gitrevs := $(shell git rev-list v$(stable)..HEAD | wc -l)
githash := $(shell git rev-parse --short HEAD)

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

all: $(sln) version
	$(build) $(basename).sln

.PHONY: clean
clean:
	$(rm) $(rmflags) $(basename).exe
	$(rm) $(rmflags) $(basename).pdb
	$(rm) $(rmflags) $(basename)/obj
	$(rm) $(rmflags) $(basename)/ProductVersion.cs

install: all $(basename)/$(script)
	$(mkdir) $(mkdirflags) $(libexec) $(localbin)
	$(cp) $(cpflags) $(basename).exe $(libexec)
	$(cp) $(cpflags) $(basename)/$(script) $(localbin)

.PHONY: uninstall
uninstall:
	$(rm) $(rmflags) $(libexec)/$(basename).exe
	$(rm) $(rmflags) $(localbin)/$(script)

.PHONY: version
version:
	$(shell echo "namespace KSPNameGen\n{\n\tpublic static class ProductVersion\n\t{\n\t\tpublic static string productVersion = \"$(stable)-$(gitrevs)-g$(githash)\";\n\t}\n}" > $(basename)/ProductVersion.cs)