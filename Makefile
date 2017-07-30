# Makefile for KSPNameGen v0.2.1
# Tested with:
# Mono JIT compiler 5.0.1.1
# Mono C# compiler 5.0.1.0

CSC = $(shell which mcs)
bin = KSPNameGen.exe
src = KSPNameGen/KSPNameGen.cs
script = KSPNameGen/kspng
platform = $(shell uname -s)

ifeq ($(platform),Linux)
    libexec = /usr/libexec
endif
ifeq ($(platform),Darwin)
    libexec = /usr/local/libexec
endif

.PHONY: all
all: $(src)
	$(CSC) $(src) -out:$(bin)

.PHONY: clean
clean:
	rm -f $(bin)

install: $(bin) $(script)
	mkdir -pf $(libexec) /usr/local/bin
	cp -f $(bin) $(libexec)
	cp -f $(script) /usr/local/bin

.PHONY: uninstall
uninstall:
	rm -f $(libexec)/$(bin)
	rm -f /usr/local/bin/kspng

