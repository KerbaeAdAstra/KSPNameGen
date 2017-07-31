# Makefile for KSPNameGen v0.2.1

# Command definitions

cp = $(shell which cp)
rm = $(shell which rm)
csc = $(shell which mcs)
mkdir = $(shell which mkdir)
platform = $(shell uname -s)

# Flag definitions

cscflags = -out:
cprmflags = -f
mkdirflags = -p

# Path definitions

bin = KSPNameGen.exe
src = KSPNameGen.cs
script = kspng
projdir = KSPNameGen
libexec = /usr/local/libexec
localbin = /usr/local/bin

.PHONY: all
all: $(projdir)/$(src)
	$(csc) $(projdir)/$(src) $(cscflags)$(bin)

.PHONY: clean
clean:
	$(rm) $(cprmflags) $(bin)

install: $(bin) $(projdir)/$(script)
	$(mkdir) $(mkdirflags) $(libexec) $(localbin)
	$(cp) $(cprmflags) $(bin) $(libexec)
	$(cp) $(cprmflags) $(projdir)/$(script) $(localbin)

.PHONY: uninstall
uninstall:
	$(rm) $(cprmflags) $(libexec)/$(bin)
	$(rm) $(cprmflags) $(localbin)/$(script)
