CSC = $(shell which mcs)
src = KSPNameGen/KSPNameGen.cs

KSPNameGen.exe: $(src)
	$(CSC) $(src) -o $@

.PHONY: clean
clean:
	rm -f KSPNameGen.exe

.PHONY: install
install: KSPNameGen.exe KSPNameGen/kspng
	cp KSPNameGen.exe /usr/libexec
	cp KSPNameGen/kspng /usr/local/bin

.PHONY: uninstall
uninstall:
	rm -f /usr/libexec/KSPNameGen.exe
	rm -f /usr/local/bin/kspng