CSC = $(shell which mcs)
src = KSPNameGen/KSPNameGen.cs

KSPNameGen.exe: $(src)
	$(CSC) $(src) -o $@

.PHONY: clean
clean:
	rm -vf KSPNameGen.exe

.PHONY: install
install: KSPNameGen.exe KSPNameGen/kspng
	cp -vf KSPNameGen.exe /usr/libexec
	cp -vf KSPNameGen/kspng /usr/local/bin

.PHONY: uninstall
uninstall:
	rm -vf /usr/libexec/KSPNameGen.exe
	rm -vf /usr/local/bin/kspng