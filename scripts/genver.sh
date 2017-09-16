#!/usr/bin/env sh
stable="0.3.1"

printf "namespace KSPNameGen\n{\n\tpublic static class ProductVersion\n\t{\n\t\tpublic const string productVersion = \"%s\";\n\t}\n}\n" "$stable" > KSPNameGen/ProductVersion.cs
