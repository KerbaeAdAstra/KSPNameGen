# Makefile for KSPNameGen v0.2.1

# Shell command variables

cp = $(shell which cp)
rm = $(shell which rm)
csc = $(shell which mcs)
mkdir = $(shell which mkdir)
platform = $(shell uname -s)

# Other variable definitions

bin = KSPNameGen.exe
src = KSPNameGen/KSPNameGen.cs
script = KSPNameGen/kspng
libexec = /usr/local/libexec

.PHONY: all
all: $(src)
	$(csc) $(src) -out:$(bin)

.PHONY: clean
clean:
	rm -f $(bin)

install: $(bin) $(script)
	mkdir -p $(libexec) /usr/local/bin
	cp -f $(bin) $(libexec)
	cp -f $(script) /usr/local/bin

.PHONY: uninstall
uninstall:
	rm -f $(libexec)/$(bin)
	rm -f /usr/local/bin/kspng
