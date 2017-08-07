// KSPNameGen.cs
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

// using statements

using System;
using System.IO;
using System.Reflection;
using System.Threading;

// warning suppression declarations

#pragma warning disable RECS0135

namespace KSPNameGen
{	
	class KSPNameGen
	{
		// array definitions

		static readonly string[] prompt = { // cache prompts
			"Specify number of names to generate.", // NMBR
			"Specify buffer size.", // Buffer
			"Specify filepath (absolute or relative)." // Filepath
		};

		static readonly ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor)); // cache colors

		// enum defs

		enum Modes { Main, Options };

		// static version defs

		const ushort MAJOR = 0;
		const ushort MINOR = 3;
		const ushort PATCH = 0;
		const string SUFFX = "";

		// variable definitions

		static readonly Random random = new Random();
		static bool gen = true;
		static int[] cursor = { 0, 0 };
		static int[] param = { 0, 0, 0, 0 };
		static bool state = true;
		static bool writeHelp;
		static Modes drawMode = Modes.Main;
		static ulong bufferSize = 48;
		static string filePath = "";
		static bool writeFile;
		static ConsoleKeyInfo input;
		const string help = "Standard names have a 'Kerman' surname, while Future-style names have" +
			"\nrandomly generated surnames." + // TYPE
			"Proper names are chosen from a list, while constructed names are" +
			"\nconstructed from a list of prefixes and suffixes.\n" +
			"If the option 'combination' is chosen, then there is a 1/20 chance" +
			"\nthat the generated name is proper."; // CMBO

		// application logic begins here

		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Loop();
			}
			
			if(Utils.FlagExists(args, "-h") || Utils.FlagExists(args, "--help"))
			{
				Usage(false);
			}
			
			string argument = "fpm";
			
			if(Utils.FlagExists(args, "-t") || Utils.FlagExists(args, "--type"))
			{
				if(Utils.FlagParse(args, "-t", out argument, argument) || Utils.FlagParse(args, "--type", out argument, argument))
				{
					param = IntArrayify(argument);
				}
				else
				{
					Usage(true);
				}
			}
			
			Utils.FlagParse(args, "-b", out bufferSize, bufferSize);
			Utils.FlagParse(args, "--buffer", out bufferSize, bufferSize);
			
			if(Utils.FlagExists(args, "-f") || Utils.FlagExists(args, "--file"))
			{
				if(Utils.FlagParse(args, "-f", out filePath, filePath) || Utils.FlagParse(args, "--file", out filePath, filePath))
				{
					if(Accessible())
					{
						writeFile = true;
					}
					else
					{
						Console.WriteLine("Invalid file.");
					}
				}
				else
				{
					Usage(true);
				}
			}
			if (Utils.FlagExists(args, "-i") || Utils.FlagExists(args, "--interactive"))
			{
				Loop();
			}
			
			ulong genNum = 0;
			
			if(Utils.FlagExists(args, "-n") || Utils.FlagExists(args, "--number"))
			{
				if(Utils.FlagParse(args, "-n", out genNum, genNum) || Utils.FlagParse(args, "--number", out genNum, genNum))
				{
					Iterator(genNum, Stringify(param), bufferSize);
				}
				else
				{
					Usage(true);
				}
			}
			else
			{
				Usage(true);
			}

			Console.ReadKey(true);
			Kill(0);
		}

		static void Usage(bool error)
		{
			string basename;
			string lockfile = "/tmp/kspng.lock";
			if (File.Exists(lockfile))
			{
				StreamReader sr = new StreamReader(lockfile);
				basename = sr.ReadLine();
				sr.Dispose();
				File.Delete(lockfile);
			}
			else
			{
				basename = Path.GetFileName(Assembly.GetEntryAssembly().Location);
			}
			Console.Write(
				"Usage: {0} [ARGUMENTS]\n" +
				"A list of valid flags and their arguments follow.\n" +
				"-h --help:        No argument. Displays this message.\n" +
				"-t --type:        A string indicating the type of name to generate. Defaults to fpm.\n" +
				"-b --buffer:      An integer indicating the number of names to write to stdout per frame.\n" +
				"-f --file:        A string indicating the output file, using either relative or absolute paths.\n" +
				"-i --interactive: No argument. Forces interactive mode; default.\n" +
				"-n --number:      An integer indicating the number of names to generate. Also noninteractive.\n" +
				"All other (invalid) flags and arguments will result in this message being shown.\n"
				, basename);
			if (error) //
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
			Draw();
			for (;;)
			{
				if (gen)
				{
					Draw();
					NameGen();
				}
				else
				{
					inString = PromptS("Would you like to generate more names? (Y/N)");
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

		static void Kill(ushort exitCode)
		{
			Console.Write("Exiting");
			for (int i = 0; i < 3; i++)
			{
				Thread.Sleep(333);
				Console.Write(".");
			}
			Console.WriteLine();
			Environment.Exit(exitCode);
		}

		static void NameGen()
		{
			switch (drawMode)
			{
				case Modes.Main:
					if (state)
					{
						input = Console.ReadKey(false);
						switch (input.Key)
						{
							case ConsoleKey.DownArrow:
								cursor[0]++;
								cursor[0] %= 4;
								cursor[1] = param[cursor[0]];
								break;

							case ConsoleKey.UpArrow:
								cursor[0]--;
								cursor[0] += 4;
								cursor[0] %= 4;
								cursor[1] = param[cursor[0]];
								break;

							case ConsoleKey.RightArrow:
								cursor[1]++;
								break;

							case ConsoleKey.LeftArrow:
								cursor[1]--;
								break;

							case ConsoleKey.Enter:
								switch (param[3])
								{
									case 0: // Generate
										state = false; //NMBR
										return;

									case 1: // Help
										writeHelp = true;
										break;

									case 2: // Exit
										Kill(0);
										break;

									case 3: // Options
										drawMode = Modes.Options;
										break;
								}
								break;
						}
						switch (cursor[0])
						{
							case 0: // TYPE
								cursor[1] += 2;
								cursor[1] %= 2;
								break;

							case 1: // CMBO
								cursor[1] += 3;
								cursor[1] %= 3;
								break;

							case 2: // GNDR
								cursor[1] += 2;
								cursor[1] %= 2;
								break;

							case 3: // ACTS
								cursor[1] += 4;
								cursor[1] %= 4;
								break;
						}
						param[cursor[0]] = cursor[1];
						return;
					}
					Iterator(PromptN(prompt[0]), Stringify(param), bufferSize);
					gen = false;
					state = true;
					break;
				case Modes.Options:
					input = Console.ReadKey(false);
					switch (input.Key)
					{
						case ConsoleKey.DownArrow:
							cursor[0]++;
							cursor[0] %= 4;
							break;

						case ConsoleKey.UpArrow:
							cursor[0]--;
							cursor[0] += 4;
							cursor[0] %= 4;
							break;

						case ConsoleKey.Enter:
							switch (cursor[0])
							{
								case 0: //Buffer size
									bufferSize = PromptN(prompt[1]);
									return;

								case 1: //Filepath
									filePath = PromptS(prompt[2]);
									break;

								case 2: //Exit
									writeFile = !writeFile;
									writeFile &= Accessible();
									break;

								case 3: //Apply
									drawMode = Modes.Main;
									break;
							}
							break;
					}
					break;
			}
		}

		static string Stringify(int[] _param)
		{
			string output = _param[0] == 0 ? "f" : "s";
			output += _param[1] == 0 ? "p" : _param[1] == 1 ? "r" : "c";
			output += _param[2] == 0 ? "m" : "f";
			return output;
		}
		
		static int[] IntArrayify(string par)
		{
			int[] output = { 0, 0, 0, 0 };
			char[] parArr = par.ToCharArray();
			if(parArr.Length != 3)
			{
				Usage(true);
			}
			output[0] = parArr[0] == 'f' ? 0 : 1;
			output[1] = parArr[1] == 'p' ? 0 : parArr[1] == 'r' ? 1 : 2;
			output[2] = parArr[2] == 'm' ? 0 : 1;
			output[3] = 0;
			return output;
		}

		static void Draw()
		{
			ConsoleColor oldBack = Console.BackgroundColor;
			ConsoleColor newBack = ConsoleColor.DarkRed;
			ConsoleColor fexBack = ConsoleColor.Green;
			ConsoleColor dneBack = ConsoleColor.Red;

			Console.Clear();
			switch (drawMode)
			{
				case Modes.Main:
					Console.WriteLine("KSPNameGen v" + MAJOR + "." + MINOR + "." + PATCH + SUFFX);

					Console.BackgroundColor = cursor[0] == 0 ? newBack : oldBack;
					Console.WriteLine(param[0] == 0 ?
							"[Future] Standard              " :
							" Future [Standard]             ");

					Console.BackgroundColor = cursor[0] == 1 ? newBack : oldBack;
					Console.WriteLine(param[1] == 0 ?
							"[Proper] Mixed  Constructed    " :
										param[1] == 1 ?
							" Proper [Mixed] Constructed    " :
							" Proper  Mixed [Constructed]   ");

					Console.BackgroundColor = cursor[0] == 2 ? newBack : oldBack;
					Console.WriteLine(param[2] == 0 ?
							"[Male] Female                  " :
							" Male [Female]                 ");

					Console.BackgroundColor = cursor[0] == 3 ? newBack : oldBack;
					Console.WriteLine(param[3] == 0 ?
							"[Generate] Help  Exit  Options " :
										param[3] == 1 ?
							" Generate [Help] Exit  Options " :
										param[3] == 2 ?
							" Generate  Help [Exit] Options " :
							" Generate  Help  Exit [Options]");

					Console.BackgroundColor = oldBack;
					if (writeHelp)
					{
					Console.WriteLine(help);
						writeHelp = false;
					}
					break;

				case Modes.Options:
					Console.WriteLine("Options");

					Console.BackgroundColor = cursor[0] == 0 ? newBack : oldBack;
					Console.WriteLine("Buffer Size:   {0,16}", bufferSize);

					Console.BackgroundColor = cursor[0] == 1 ? newBack : oldBack;
					Console.WriteLine("File Path:                     ");
					Console.BackgroundColor = Accessible() ? fexBack : dneBack;
					Console.WriteLine("{0,31}", filePath);

					Console.BackgroundColor = cursor[0] == 2 ? newBack : oldBack;
					Console.WriteLine(writeFile ?
						"Write to File               [x]" :
						"Write to File               [ ]");

					Console.BackgroundColor = cursor[0] == 3 ? newBack : oldBack;
					Console.WriteLine(cursor[0] == 3 ?
						"[Apply]                        " :
						" Apply                         ");

					Console.BackgroundColor = oldBack;
					break;
			}
		}

		static bool Accessible()
		{
			if (!File.Exists(filePath))
				return false;
			try
			{
				File.OpenWrite(filePath).Close();
			}
			catch
			{
				return false;
			}

			return true;
		}

		static ulong PromptN(string query)
		{
			ulong inputLong = 0;
			Console.WriteLine(query);
			if (!ulong.TryParse(Console.ReadLine(), out inputLong))
			{
				Console.WriteLine("A positive integer was not specified.");
			}
			return inputLong;
		}

		static string PromptS(string query)
		{
			Console.WriteLine(query);
			return Console.ReadLine().ToLower();
		}

		static string Generate(string _param)
		{
			bool toggle = random.Next(20) == 0;
			switch (_param)
			{
				case "spf":
					return NameArrays.fpr[random.Next(NameArrays.fpr.Length)] + " Kerman";

				case "spm":
					return NameArrays.mpr[random.Next(NameArrays.mpr.Length)] + " Kerman";

				case "scf":
					return NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcp.Length)] + " Kerman";

				case "scm":
					return NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcp.Length)] + " Kerman";

				case "srf":
					if (toggle)
						return NameArrays.fpr[random.Next(NameArrays.fpr.Length)] + " Kerman";
					return NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcp.Length)] + " Kerman";

				case "srm":
					if (toggle)
						return NameArrays.mpr[random.Next(NameArrays.mpr.Length)] + " Kerman";
					return NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcp.Length)] + " Kerman";

				case "fpf":
					return NameArrays.fpr[random.Next(NameArrays.fpr.Length)] + " " + NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcs.Length)];

				case "fpm":
					return NameArrays.mpr[random.Next(NameArrays.mpr.Length)] + " " + NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcs.Length)];

				case "fcf":
					return NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcs.Length)] + " " + NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcs.Length)];

				case "fcm":
					return NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcs.Length)] + " " + NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcs.Length)];

				case "frf":
					if (toggle)
						return NameArrays.fpr[random.Next(NameArrays.fpr.Length)] + " " + NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcs.Length)];
					return NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcs.Length)] + " " + NameArrays.fcp[random.Next(NameArrays.fcp.Length)] + NameArrays.fcs[random.Next(NameArrays.fcs.Length)];

				case "frm":
					if (toggle)
						return NameArrays.mpr[random.Next(NameArrays.mpr.Length)] + " " + NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcs.Length)];
					return NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcs.Length)] + " " + NameArrays.mcp[random.Next(NameArrays.mcp.Length)] + NameArrays.mcs[random.Next(NameArrays.mcs.Length)];
				default:
					return null; // this should not ever happen, but it's just there to stop Mono from throwing a fit
			}
		}

		static void Nyan(ulong inputLong)
		{
			ConsoleColor currentBackground = Console.BackgroundColor;
			string Generated = "";
			for (ulong i = 0; i < inputLong / 15; i++)
			{
				foreach (ConsoleColor color in colors)
				{
					if (color == currentBackground)
					{
						continue;
					}
					Console.ForegroundColor = color;
					Generated = Generate(Stringify(param));
					Console.WriteLine(Generated);
				}
			}
			gen = false;
			param = new int[]{ 0, 0, 0 };
			Loop();
		}


		static void Iterator(ulong number, string _param, ulong buffsize)
		{
			if (number > uint.MaxValue)
			{
				Nyan(number);
				return;
			}
			if (writeFile)
			{
				StreamWriter sr = File.CreateText(filePath);
				for (ulong i = 0; i < number; i++)
				{
					sr.WriteLine(Generate(_param));
				}
				sr.Dispose();
				Console.WriteLine("Complete.");
				return;
			}

			string buffer = "";
			string Generated = "";
			for (ulong i = 0; i < number; i++)
			{
				Generated = Generate(_param);
				buffer += Generated + "\n";
				if (i % buffsize == 0)
				{
					Console.Write(buffer);
					buffer = "";
				}
			}
			Console.Write(buffer);
		}
	}
}
