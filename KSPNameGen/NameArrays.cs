// NameArrays.cs
//
// This file is part of KSPNameGen, a free (gratis and libre) name generator for Kerbal Space Program.
// Kerbal Space Program is (c) 2011-2017 Squad. All Rights Reserved.
// KSPNameGen is (c) 2016-2017 the Kerbae ad Astra group <kerbaeadastra@gmail.com>.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

// using directives

using System;

namespace KSPNameGen
{
    public static class NameArrays
    {
		public static readonly string[] fcp = { // female constructed prefix
            "Aga", "Al", "An", "Ar", "As", "Bar", "Bea", "Ber", "Car", "Cat",
			"Cer", "Clau", "Cris", "Da", "Dan", "Daph", "De", "Deb", "Di", "Eil",
			"Eli", "Eri", "Fran", "Gem", "Ger", "Gi", "Gil", "Gle", "Gra", "Gwen",
			"Hai", "Hay", "Hel", "Hil", "Ir", "Isa", "Ja", "Jan", "Jen", "Jes",
			"Jo", "Jul", "Ka", "Kar", "Kat", "Kath", "Ker", "Kim", "La", "Lager",
			"Le", "Lea", "Lee", "Lin", "Lis", "Liz", "Lu", "Ma", "Mad", "Mag",
			"Mar", "Mau", "Max", "Meg", "Mel", "Mi", "Mia", "Mil", "Mir", "Mo",
			"Na", "Nata", "Ne", "Pa", "Pho", "Ra", "Ro", "Ros", "Sam", "San",
			"Si", "Sie", "Sig", "Sta", "Stel", "Su", "Tam", "Tan", "Te", "Ti",
			"Tra", "Tri", "Ur", "Val", "Ver", "Vir", "Wen", "Wil", "Zel"
		};

		public static readonly string[] fcs = { // female constructed suffix
        "a", "alla", "an", "anda", "anna", "anne", "ayne", "be", "bel",
			"bella", "belle", "berta", "beth", "bie", "by", "ca", "cee", "cella",
			"chel", "chell", "chelle", "cia", "cie", "cine", "cy", "da", "di",
			"dia", "die", "dine", "dolin", "dra", "drien", "e", "edith", "ee",
			"ela", "ella", "elle", "elyn", "emma", "ena", "ene", "enna", "erta",
			"et", "ette", "gee", "gela", "gie", "grid", "gy", "i", "iana", "ica",
			"icca", "ice", "ie", "iel", "iella", "igh", "in", "ina", "ine",
			"inne", "is", "isa", "ise", "issa", "jorie", "ke", "la", "lee",
			"lenna", "lian", "liana", "lie", "lin", "lina", "line", "linne",
			"lla", "llian", "lotte", "ly", "lyn", "ma", "mie", "mma", "my", "na",
			"na", "nda", "ne", "nica", "nie", "nna", "nne", "ny", "otte", "phe",
			"phia", "phie", "ra", "re", "ree", "righ", "rina", "rine", "ris",
			"rix", "rude", "ry", "rys", "sa", "san", "sei", "selle", "sha", "sie",
			"sy", "t", "ta", "te", "tha", "this", "thy", "ti", "tina", "tine",
			"trice", "trid", "tris", "trix", "trude", "tte", "ty", "uki", "ula",
			"una", "vie", "vy", "xie", "xy", "y", "ya", "yin", "yn", "yne", "ys",
			"zie", "zy"
		};

		public static readonly string[] fpr = { // female proper name
            "Alice", "Barbara", "Bonnie", "Brooke", "Carol", "Dottie", "Dotty",
			"Eileen", "Ellen", "Heidi", "Jane", "Jean", "Jeaneane", "Jeanette",
			"Joan", "Judith", "Karen", "Leah", "Leia", "Lisa", "Lola", "Margaret",
			"Maya", "Megan", "Mila", "Nancy", "Nicole", "Nina", "Olivia", "Peggy",
			"Phoebe", "Piper", "Sally", "Samantha", "Sara", "Sarah", "Serena",
			"Shannon", "Stephanie", "Summer", "Svetlana", "Tamara", "Tati",
			"Tatyana", "Valentina"
		};

		public static readonly string[] mcp = { // male constructed prefix
            "Ad", "Al", "Ald", "An", "Bar", "Bart", "Bil", "Billy-Bob", "Bob",
			"Bur", "Cal", "Cam", "Chad", "Cor", "Dan", "Der", "Des", "Dil", "Do",
			"Don", "Dood", "Dud", "Dun", "Ed", "El", "En", "Er", "Fer", "Fred",
			"Gene", "Geof", "Gil", "Greg", "Gus", "Had", "Hal", "Han", "Har",
			"Hen", "Her", "Hud", "Jed", "Jen", "Jer", "Joe", "John", "Jon", "Jor",
			"Kel", "Ken", "Ker", "Kir", "Lan", "Lem", "Len", "Lo", "Lod", "Lu",
			"Lud", "Mac", "Mal", "Mat", "Mel", "Mer", "Mil", "Mit", "Mun", "Ned",
			"Neil", "Nel", "New", "Ob", "Or", "Pat", "Phil", "Ray", "Rib", "Rich",
			"Ro", "Rod", "Ron", "Sam", "Sean", "See", "Shel", "Shep", "Sher",
			"Sid", "Sig", "Son", "Thom", "Thomp", "Tom", "Wehr", "Wil"
		};

		public static readonly string[] mcs = { // male constructed suffix
            "ald", "bal", "bald", "bart", "bas", "berry", "bert", "bin", "ble",
			"bles", "bo", "bree", "brett", "bro", "bur", "burry", "bus", "by",
			"cal", "can", "cas", "cott", "dan", "das", "den", "din", "do", "don",
			"dorf", "dos", "dous", "dred", "drin", "dun", "ely", "emone", "emy",
			"eny", "fal", "fel", "fen", "field", "ford", "fred", "frey", "frey",
			"frid", "frod", "fry", "furt", "gan", "gard", "gas", "gee", "gel",
			"ger", "gun", "hat", "ing", "ke", "kin", "lan", "las", "ler", "ley",
			"lie", "lin", "lin", "lo", "lock", "long", "lorf", "ly", "mal", "man",
			"min", "ming", "mon", "more", "mund", "my", "nand", "nard", "ner",
			"ney", "nie", "ny", "oly", "ory", "rey", "rick", "rie", "righ", "rim",
			"rod", "ry", "sby", "sel", "sen", "sey", "ski", "son", "sted", "ster",
			"sy", "ton", "top", "trey", "van", "vey", "vin", "vis", "well", "wig",
			"win", "wise", "zer", "zon", "zor"
		};

		public static readonly string[] mpr = { // male proper name
            "Adam", "Al", "Alan", "Archibald", "Buzz", "Carson", "Chad", "Charlie",
			"Chris", "Chuck", "Dean", "Ed", "Edan", "Edlu", "Frank", "Franklin",
			"Gus", "Hans", "Jack", "James", "Jim", "Kirk", "Kurt", "Lars", "Luke",
			"Mac", "Matt", "Phil", "Randall", "Scott", "Scott", "Sean", "Steve",
			"Tom", "Will"
		};
    }
}
