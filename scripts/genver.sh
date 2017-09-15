#!/usr/bin/env sh
stable="0.3.0"
gitrevs="$(git rev-list v$stable..HEAD 2> /dev/null | wc -l)"
githash="$(git rev-parse --short HEAD 2> /dev/null)"

if [ "$gitrevs" -eq "0" ]; then
    gitrevs="."
fi

gitrevs="."$gitrevs

printf "namespace KSPNameGen\n{\n\tpublic static class ProductVersion\n\t{\n\t\tpublic const string productVersion = \"%s%s-g%s\";\n\t}\n}" "$stable" "$gitrevs" "$githash" > KSPNameGen/ProductVersion.cs