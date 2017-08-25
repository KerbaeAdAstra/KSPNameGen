// KSPNameGen.cs
//
// This file is part of KSPNameGen, a free (gratis and libre) name generator for
// Kerbal Space Program.
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
using static System.Console;
using static System.ConsoleKey;
using static System.IO.File;
using static KSPNameGen.NameArrays;
using static KSPNameGen.Utils;

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

		static readonly ConsoleColor[] colors =
			(ConsoleColor[])Enum.GetValues(typeof(ConsoleColor)); // cache colors

		// enum defs

		enum Modes { Main, Options };

		// static version def

		static readonly string productVersion =
			typeof(KSPNameGen).Assembly.GetCustomAttribute
							  <AssemblyInformationalVersionAttribute>()
							  .InformationalVersion;

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
		const string help = "Standard names have a 'Kerman' surname, while" +
			"Future-style names have\nrandomly generated surnames." + // TYPE
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
			
			if (FlagExists(args, "-h") || FlagExists(args, "--help"))
			{
				Usage(false);
			}

			if (FlagExists(args, "-v") || FlagExists(args, "--version"))
			{
				Version();
			}
			
			string argument = "fpm";
			
			if (FlagExists(args, "-t") || FlagExists(args, "--type"))
			{
				if (FlagParse(args, "-t", out argument, argument) ||
					FlagParse(args, "--type", out argument, argument))
				{
					param = IntArrayify(argument);
				}
				else
				{
					Usage(true);
				}
			}
			
			FlagParse(args, "-b", out bufferSize, bufferSize);
			FlagParse(args, "--buffer", out bufferSize, bufferSize);
			
			if (FlagExists(args, "-f") || FlagExists(args, "--file"))
			{
				if (FlagParse(args, "-f", out filePath, filePath) ||
					FlagParse(args, "--file", out filePath, filePath))
				{
					if (Accessible())
					{
						writeFile = true;
					}
					else
					{
						WriteLine("A writable file was not specified.");
					}
				}
				else
				{
					Usage(true);
				}
			}
			if (FlagExists(args, "-i") || FlagExists(args, "--interactive"))
			{
				Loop();
			}
			
			ulong genNum = 0;
			
			if (FlagExists(args, "-n") || FlagExists(args, "--number"))
			{
				if (FlagParse(args, "-n", out genNum, genNum) ||
					FlagParse(args, "--number", out genNum, genNum))
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

			ReadKey(true);
			Kill(0);
		}

		static string GetBasename()
		{
			string lockfile = "/tmp/kspng.lock";
			if (Exists(lockfile))
			{
				StreamReader sr = new StreamReader(lockfile);
				string basename = sr.ReadLine();
				sr.Dispose();
				Delete(lockfile);
				return basename;
			}
			return Path.GetFileName(Assembly.GetEntryAssembly().Location);
		}

		static void Usage(bool error)
		{
			Write(
				"Usage: {0} [flags] [args]\n" +
				"A list of valid flags and their arguments follow.\n" +
				"-h --help:        No argument. Displays this message.\n" +
				"-t --type:        A string indicating the type of name to " +
				"generate. Defaults to fpm.\n" +
				"-b --buffer:      An integer indicating the number of names to " +
				"write to stdout per frame.\n" +
				"-f --file:        A string indicating the output file, using " +
				"either relative or absolute paths.\n" +
				"-i --interactive: No argument. Forces interactive mode; default.\n" +
				"-n --number:      An integer indicating the number of names to " +
				"generate. Also noninteractive.\n" +
				"All other (invalid) flags and arguments will result in this " +
				"message being shown.\n"
				, GetBasename());
			if (error) //
			{
				Kill(1);
			}
			else
			{
				Kill(0);
			}
		}

		static void Version()
		{
			Write(
				"KSPNameGen version {0}" +
				"\nCopyright (c) 2016-2017 the Kerbae ad Astra group." +
				"\nLicense MIT: The MIT License <https://opensource.org/licenses/MIT>" +
				"\nThis is free software; you are free to change and" +
				"redistribute it if and only if you include the license terms " +
				"stated above when redistributing." +
				"\nThere is NO WARRANTY, to the extent permitted by law.\n"
				, productVersion);
			Kill(0);
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
					inString = PromptS("Would you like to generate more names? " +
									   "(Y/N)");
					switch (inString)
					{
						case "y":
							gen = true;
							break;

						case "n":
							Kill(0);
							break;

						default:
							WriteLine("Invalid response.");
							break;
					}
				}
			}
		}

		static void Kill(ushort exitCode)
		{
			Write("Exiting");
			for (int i = 0; i < 3; i++)
			{
				Thread.Sleep(333);
				Write(".");
			}
			WriteLine();
			Environment.Exit(exitCode);
		}

		static void NameGen()
		{
			switch (drawMode)
			{
				case Modes.Main:
					if (state)
					{
						input = ReadKey(false);
						switch (input.Key)
						{
							case DownArrow:
								cursor[0]++;
								cursor[0] %= 4;
								cursor[1] = param[cursor[0]];
								break;

							case UpArrow:
								cursor[0]--;
								cursor[0] += 4;
								cursor[0] %= 4;
								cursor[1] = param[cursor[0]];
								break;

							case RightArrow:
								cursor[1]++;
								break;

							case LeftArrow:
								cursor[1]--;
								break;

							case Enter:
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
					Iterator(PromptI(prompt[0]), Stringify(param), bufferSize);
					gen = false;
					state = true;
					break;
				case Modes.Options:
					input = ReadKey(false);
					switch (input.Key)
					{
						case DownArrow:
							cursor[0]++;
							cursor[0] %= 4;
							break;

						case UpArrow:
							cursor[0]--;
							cursor[0] += 4;
							cursor[0] %= 4;
							break;

						case Enter:
							switch (cursor[0])
							{
								case 0: //Buffer size
									bufferSize = PromptI(prompt[1]);
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
			ConsoleColor oldBack = BackgroundColor;
			ConsoleColor newBack = ConsoleColor.DarkRed;
			ConsoleColor fexBack = ConsoleColor.Green;
			ConsoleColor dneBack = ConsoleColor.Red;

			Clear();
			switch (drawMode)
			{
				case Modes.Main:
					WriteLine("KSPNameGen v{0}", productVersion);

					BackgroundColor = cursor[0] == 0 ? newBack : oldBack;
					WriteLine(param[0] == 0 ?
							"[Future] Standard              " :
							" Future [Standard]             ");

					BackgroundColor = cursor[0] == 1 ? newBack : oldBack;
					WriteLine(param[1] == 0 ?
							"[Proper] Mixed  Constructed    " :
										param[1] == 1 ?
							" Proper [Mixed] Constructed    " :
							" Proper  Mixed [Constructed]   ");

					BackgroundColor = cursor[0] == 2 ? newBack : oldBack;
					WriteLine(param[2] == 0 ?
							"[Male] Female                  " :
							" Male [Female]                 ");

					BackgroundColor = cursor[0] == 3 ? newBack : oldBack;
					WriteLine(param[3] == 0 ?
							"[Generate] Help  Exit  Options " :
										param[3] == 1 ?
							" Generate [Help] Exit  Options " :
										param[3] == 2 ?
							" Generate  Help [Exit] Options " :
							" Generate  Help  Exit [Options]");

					BackgroundColor = oldBack;
					if (writeHelp)
					{
					WriteLine(help);
						writeHelp = false;
					}
					break;

				case Modes.Options:
					WriteLine("Options");

					BackgroundColor = cursor[0] == 0 ? newBack : oldBack;
					WriteLine("Buffer Size:   {0,16}", bufferSize);

					BackgroundColor = cursor[0] == 1 ? newBack : oldBack;
					WriteLine("File Path:                     ");
					BackgroundColor = Accessible() ? fexBack : dneBack;
					WriteLine("{0,31}", filePath);

					BackgroundColor = cursor[0] == 2 ? newBack : oldBack;
					WriteLine(writeFile ?
						"Write to File               [x]" :
						"Write to File               [ ]");

					BackgroundColor = cursor[0] == 3 ? newBack : oldBack;
					WriteLine(cursor[0] == 3 ?
						"[Apply]                        " :
						" Apply                         ");

					BackgroundColor = oldBack;
					break;
			}
		}

		static bool Accessible()
		{
			if (!Exists(filePath))
				return false;
			try
			{
				OpenWrite(filePath).Close();
			}
			catch
			{
				return false;
			}

			return true;
		}

		static string Generate(string _param)
		{
			bool toggle = random.Next(20) == 0;
			switch (_param)
			{
				case "spf":
					return fpr[random.Next(fpr.Length)] + " Kerman";

				case "spm":
					return mpr[random.Next(mpr.Length)] + " Kerman";

				case "scf":
					return fcp[random.Next(fcp.Length)] + fcs
						[random.Next(fcp.Length)] + " Kerman";

				case "scm":
					return mcp[random.Next(mcp.Length)] + mcs
						[random.Next(mcp.Length)] + " Kerman";

				case "srf":
					if (toggle)
						return fpr[random.Next(fpr.Length)] + " Kerman";
					return fcp[random.Next(fcp.Length)] + fcs
						[random.Next(fcp.Length)] + " Kerman";

				case "srm":
					if (toggle)
						return mpr[random.Next(mpr.Length)] + " Kerman";
					return mcp[random.Next(mcp.Length)] + mcs
						[random.Next(mcp.Length)] + " Kerman";

				case "fpf":
					return fpr[random.Next(fpr.Length)] + " " + fcp
						[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];

				case "fpm":
					return mpr[random.Next(mpr.Length)] + " " + mcp
						[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];

				case "fcf":
					return fcp[random.Next(fcp.Length)] + fcs
						[random.Next(fcs.Length)] + " " + 
						fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];

				case "fcm":
					return mcp[random.Next(mcp.Length)] + 
						mcs[random.Next(mcs.Length)] + " " + 
						mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];

				case "frf":
					if (toggle)
						return fpr[random.Next(fpr.Length)] + " " + 
							fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];
					return fcp[random.Next(fcp.Length)] + 
						fcs[random.Next(fcs.Length)] + " " + 
						fcp[random.Next(fcp.Length)] + fcs[random.Next(fcs.Length)];

				case "frm":
					if (toggle)
						return mpr[random.Next(mpr.Length)] + " " + 
							mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];
					return mcp[random.Next(mcp.Length)] + 
						mcs[random.Next(mcs.Length)] + " " + 
						mcp[random.Next(mcp.Length)] + mcs[random.Next(mcs.Length)];
				default:
					return null; // this should not ever happen, but it's just
								 // there to stop Mono from throwing a fit
			}
		}

		static void Nyan(ulong inputLong)
		{
			ConsoleColor currentBackground = BackgroundColor;

			for (ulong i = 0; i < inputLong / 15; i++)
			{
				foreach (ConsoleColor color in colors)
				{
					if (color == currentBackground)
					{
						continue;
					}
					ForegroundColor = color;
					WriteLine(Generate(Stringify(param)));
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
				StreamWriter sr = CreateText(filePath);
				for (ulong i = 0; i < number; i++)
				{
					sr.WriteLine(Generate(_param));
				}
				sr.Dispose();
				WriteLine("Complete.");
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
					Write(buffer);
					buffer = "";
				}
			}
			Write(buffer);
		}
	}
}
