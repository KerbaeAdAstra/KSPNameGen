CSC = $(shell which mcs)
src = KSPNameGen/KSPNameGen.cs
platform = $(shell uname -s)

ifeq ($(platform),Linux)
    libexec = /usr/libexec
endif
ifeq ($(platform),Darwin)
    libexec = /usr/local/libexec
endif

KSPNameGen.exe: $(src)
	$(CSC) $(src) -o $@

.PHONY: clean
clean:
	rm -vf KSPNameGen.exe

.PHONY: install
install: KSPNameGen.exe KSPNameGen/kspng
	cp -vf KSPNameGen.exe $(libexec)/
	cp -vf KSPNameGen/kspng /usr/local/bin

.PHONY: uninstall
uninstall:
	rm -vf $(libexec)/KSPNameGen.exe
	rm -vf /usr/local/bin/kspng