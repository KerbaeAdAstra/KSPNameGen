/*
 * KSPNameGen: name generator for Kerbal Space Program
 * Kerbal Space Program is (c) 2011-2017 Squad. All Rights Reserved.
 * KSPNameGen is (c) 2016-2017 the Kerbae ad Astra group.
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;

namespace KSPNameGen
{
	class KSPNameGen
	{
		// array definitions

		static readonly string[] fcp = { // female constructed prefix
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

		static readonly string[] fcs = { // female constructed suffix
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

		static readonly string[] fpr = { // female proper name
			"Alice", "Barbara", "Bonnie", "Brooke", "Carol", "Dottie", "Dotty",
			"Eileen", "Ellen", "Heidi", "Jane", "Jean", "Jeaneane", "Jeanette",
			"Joan", "Judith", "Karen", "Leah", "Leia", "Lisa", "Lola", "Margaret",
			"Maya", "Megan", "Mila", "Nancy", "Nicole", "Nina", "Olivia", "Peggy",
			"Phoebe", "Piper", "Sally", "Samantha", "Sara", "Sarah", "Serena",
			"Shannon", "Stephanie", "Summer", "Svetlana", "Tamara", "Tati",
			"Tatyana", "Valentina"
		};

		static readonly string[] mcp = { // male constructed prefix
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

		static readonly string[] mcs = { // male constructed suffix
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

		static readonly string[] mpr = { // male proper name
			"Adam", "Al", "Alan", "Archibald", "Buzz", "Carson", "Chad", "Charlie",
			"Chris", "Chuck", "Dean", "Ed", "Edan", "Edlu", "Frank", "Franklin",
			"Gus", "Hans", "Jack", "James", "Jim", "Kirk", "Kurt", "Lars", "Luke",
			"Mac", "Matt", "Phil", "Randall", "Scott", "Scott", "Sean", "Steve",
			"Tom", "Will"
		};

		static readonly string[] prompt = { // cache prompts
			"Specify types of names to generate.\n" +
			"Type 'f' for Future-style names, or 's' for standard names.", // TYPE
			"Specify if you want to generate:\n" +
			" r: a combination of constructed and proper names\n" +
			" c: constructed names only, or\n" +
			" p: proper names only", // CMBO
			"Specify gender of generated names.\n" +
			"Type 'm' for male, or 'f' for female.", // GNDR
			"Specify number of names to generate." // NMBR
		};

		static readonly string[] help = { // cache help topics
			"Standard names have a 'Kerman' surname, while Future-style names have" +
			" randomly generated surnames.", // TYPE
			"Proper names are chosen from a list, while constructed names are" +
			"constructed from a list of prefixes and suffixes.\n" +
			"If the option 'combination' is chosen, then there is a 1/20 chance" +
			"that the generated name is proper." // CMBO
		};

		static readonly string[] validParams = { // cache valid parameters
			"spf", "spm", "scf", "scm", "srf", "srm", "fpf", "fpm", "fcf", "fcm", "frf", "frm"
		};

		static readonly ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor)); // cache colors

		// static version defs

		static readonly ushort MAJOR = 0;
		static readonly ushort MINOR = 1;
		static readonly ushort PATCH = 2;
		static readonly string SUFFX = "";

		// variable definitions

		static readonly Random random = new Random();
		static string param = "";
		static ushort inpar = 0;
		static bool gen = true;
		static bool firstRun = true;

		// application logic begins here

		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Loop();
			}

			else if (args[0] == "-i" || args[0] == "--interactive")
			{
				Loop();
			}

			else if (args.Length >= 3 && (args[0] == "-n" || args[0] == "--non-interactive"))
			{

				ulong inputULong;
				if (!ulong.TryParse(args[2], out inputULong))
				{
					Console.WriteLine("A positive integer was not specified.");
					Kill(1);
				}
				Console.WriteLine("KSPNameGen v" + MAJOR + "." + MINOR + "." + PATCH + SUFFX);
				if (validParams.Contains(args[1]))
				{
					Iterator(inputULong, args[1]);
				}
				else
				{
					Console.WriteLine("Specified type is invalid.");
					Kill(1);
				}
				Console.WriteLine("Complete.");
			}

			else if (args[0] == "-h" || args[0] == "--help")
			{
				Usage(false);
			}

			else
			{
				Usage(true);
			}

			Console.ReadKey();
			Kill(0);
		}

		static void Usage(bool error)
		{
			Console.Write("Usage: KSPNameGen.exe [-i|--interactive] [-n|--non-interactive parameter number] [-h|--help]\n\n" +
			"-i, --interactive: interactive mode (default option if no parameter specified)\n" +
			"-n, --non-interactive: non-interactive mode\n" +
			"-h, --help: show this help\n" +
			"parameter: either of [f|s] [r|c|p] [m|f] in this order. Run in interactive mode to learn more.\n" +
			"number: a nonzero integer less than 18,446,744,073,709,551,615 (2^64-1).\n" +
			"`parameter' and `number' are only used with non-interactive mode.\n\n");
			if (error)
			{
				Kill(1);
			}
			else
			{
				Kill(0);
			}
		}

		static void Loop()
		{
			string inString;
			for (;;)
			{
				if (gen)
				{
					nameGen();
					Draw();
				}
				else
				{
					inString = PromptS("Would you like to generate more names? (Y/N)", false);
					switch (inString)
					{
						case "y":
							gen = true;
							break;

						case "n":
							Kill(0);
							break;

						default:
							Console.WriteLine("Invalid response.");
							break;
					}
				}
			}
		}

		static string PromptS(string query, bool help)
		{
			Console.WriteLine(query);
			if (help)
			{
				Console.WriteLine("Type 'help' for help; 'exit' to exit.");
			}
			return Console.ReadLine().ToLower();
		}

		static ulong PromptN(string query)
		{
			ulong inputULong = 0;
			Console.WriteLine(query);
			string input = Console.ReadLine();
			if (!ulong.TryParse(input, out inputULong))
			{
				Console.WriteLine("A positive integer was not specified.");
			}
			return inputULong;
		}

		static void Kill(ushort exitCode)
		{
			Console.Write("Exiting");
			for (int i = 0; i < 3; i++)
			{
				Thread.Sleep(200);
				Console.Write(". ");
			}

			Console.WriteLine();
			Environment.Exit(exitCode);
		}

		static void nameGen()
		{
			string inString;
			ulong inULong;

			switch (inpar)
			{
				case 0: // TYPE
					Console.Clear();
					if (firstRun)
					{
						Console.WriteLine("KSPNameGen v" + MAJOR + "." + MINOR + "." + PATCH + SUFFX);
					}
					firstRun = false;
					inString = PromptS(prompt[inpar], true);
					switch (inString)
					{
						case "exit":
							Kill(0);
							break;

						case "help":
							Console.WriteLine(help[inpar]);
							break;

						case "f":
							param = "f";
							inpar = 1; // CMBO
							break;

						case "s":
							param = "s";
							inpar = 1; // CMBO
							break;

						default:
							Console.WriteLine("Specified type is invalid.");
							break;
					}
					break;

				case 1: // CMBO
					inString = PromptS(prompt[inpar], true);
					switch (inString)
					{
						case "exit":
							Kill(0);
							break;

						case "help":
							Console.WriteLine(help[inpar]);
							break;

						case "r":
							param += "r";
							inpar = 2; // GNDR
							break;

						case "c":
							param += "c";
							inpar = 2; // GNDR
							break;

						case "p":
							param += "p";
							inpar = 2; // GNDR
							break;

						default:
							Console.WriteLine("Specified modifier is invalid.");
							break;
					}
					break;

				case 2: // GNDR
					inString = PromptS(prompt[inpar], false);
					switch (inString)
					{
						case "exit":
							Kill(0);
							break;

						case "help":
							Console.WriteLine("Help is not available for this topic.");
							break;

						case "f":
							param += "f";
							inpar = 3; // NMBR
							break;

						case "m":
							param += "m";
							inpar = 3; // NMBR
							break;

						default:
							Console.WriteLine("Specified gender is invalid.");
							break;
					}
					break;

				case 3: // NMBR
					inULong = PromptN(prompt[inpar]);

					if (inULong >= 281474976710656)
					{
						Console.WriteLine("Are you sure you wish to generate " + inULong + " names? (Y/N)");
						Console.WriteLine("Generating " + inULong + " names may take a long time.");
						string genYN = Console.ReadLine().ToLower();
						if (genYN == "n")
						{
							gen = false;
							Loop();
						}
						else if (genYN == "y")
						{
							Nyan(inULong);
						}
						else
						{
							Console.WriteLine("Invalid selection.");
							break;
						}
					}

					if (inULong >= 4294967296)
					{
						Console.WriteLine("Are you sure you wish to generate " + inULong + " names? (Y/N)");
						Console.WriteLine("Generating " + inULong + " names may take a long time.");
						string genYN = Console.ReadLine().ToLower();
						if (genYN == "n")
						{
							gen = false;
							Loop();
						}
						else if (genYN == "y")
						{
							Iterator(inULong, param);
							gen = false;
							param = "";
							inpar = 0;
							Loop();
						}
						else
						{
							Console.WriteLine("Invalid selection.");
							break;
						}
					}

					if (inULong == 0)
					{
						Console.WriteLine("Specified number must be nonzero.");
						break;
					}

					Iterator(inULong, param);
					gen = false;
					param = "";
					inpar = 0;
					Loop();
					break;
			}
		}

		static void Draw()
		{
			char[] paramCharArray = param.ToCharArray();
			if (paramCharArray.Length == 0) // param is empty
				return;
			Console.Clear();
			Console.WriteLine(paramCharArray[0] == 'f' ?
					"[Future]  Standard" :
					"Future  [Standard]");
			if (paramCharArray.Length == 1) // param has only type
				return;
			Console.WriteLine(paramCharArray[1] == 'r' ?
					"Proper  [Mixed]  Constructed" :
			paramCharArray[1] == 'p' ?
					"[Proper]  Mixed  Constructed" :
					"Proper  Mixed  [Constructed]");
			if (paramCharArray.Length == 2) // param does not have gender
				return;
			Console.WriteLine(paramCharArray[2] == 'm' ?
					"[Male]  Female" :
					"Male  [Female]");
		}

		static string Generate(string param)
		{
			Contract.Requires(param != null);
			if (param == null)
				throw new ArgumentNullException(nameof(param));
			bool toggle = random.Next(20) == 0;
			switch (param)
			{
				case "spf":
					return fpr[random.Next(fpr.Length)] + " Kerman";

				case "spm":
					return mpr[random.Next(mpr.Length)] + " Kerman";

				case "scf":
					return fcp[random.Next(fcp.Length)] + fcs[random.Next(fcp.Length)] + " Kerman";

				case "scm":
					return mcp[random.Next(mcp.Length)] + mcs[random.Next(mcp.Length)] + " Kerman";

				case "srf":
					if (toggle)
					{
						return fpr[random.Next(fpr.Length)] + " Kerman";
					}
					else
					{
						return fcp[random.Next(fcp.Length)] + fcs[random.Next(fcp.Length)] + " Kerman";
					}

				case "srm":
					if (toggle)
					{
						return mpr[random.Next(mpr.Length)] + " Kerman";
					}
					else
					{
						return mcp[random.Next(mcp.Length)] + mcs[random.Next(mcp.Length)] + " Kerman";
					}

				case "fpf":
					return fpr[random.Next(fpr.Length)] + " " + fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];

				case "fpm":
					return mpr[random.Next(mpr.Length)] + " " + mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];

				case "fcf":
					return fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)] + " " + fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];

				case "fcm":
					return mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)] + " " + mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];

				case "frf":
					if (toggle)
					{
						return fpr[random.Next(fpr.Length)] + " " + fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];
					}
					else
					{
						return fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)] + " " + fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];
					}

				case "frm":
					if (toggle)
					{
						return mpr[random.Next(mpr.Length)] + " " + mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];
					}
					else
					{
						return mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)] + " " + mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];
					}
				default:
					return null; // this should not ever happen, but it's just there to stop Mono from throwing a fit
			}
		}

		static void Nyan(ulong inputULong)
		{
			ConsoleColor currentBackground = Console.BackgroundColor;
			string Generated = "";
			for (ulong i = 0; i < inputULong / 15; i++)
			{
				foreach (var color in colors)
				{
					if (color == currentBackground)
					{
						continue;
					}
					Console.ForegroundColor = color;
					Generated = Generate(param);
					Console.WriteLine(Generated);
				}
			}
			gen = false;
			param = "";
			inpar = 0;
			Loop();
		}


		static void Iterator(ulong inputULong, string param)
		{
			string buffer = "";
			string Generated = "";
			for (ulong i = 0; i < inputULong; i++)
			{
				Generated = Generate(param);
				buffer += Generated + "\n";
				if (i % 48 == 0)
				{
					Console.Write(buffer);
					buffer = "";
				}
			}
			Console.Write(buffer);
		}
	}
}
