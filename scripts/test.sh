#!/usr/bin/env sh
set -x
make all
mono KSPNameGen.exe -v
mono KSPNameGen.exe -h
