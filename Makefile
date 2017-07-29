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
	$(CSC) $(src) -out:$@

.PHONY: clean
clean:
	rm -f KSPNameGen.exe

.PHONY: install
install: KSPNameGen.exe KSPNameGen/kspng
	cp -f KSPNameGen.exe $(libexec)
	cp -f KSPNameGen/kspng /usr/local/bin

.PHONY: uninstall
uninstall:
	rm -f $(libexec)/KSPNameGen.exe
	rm -f /usr/local/bin/kspng