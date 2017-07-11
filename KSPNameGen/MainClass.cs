//
// MainClass.cs
//
// Author:
//	   Kerbae ad Astra <kerbaeadastra@gmail.com>
//
// Copyright (c) 2017 the Kerbae ad Astra group
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
using System;
using System.Linq;
using Gtk;

namespace KSPNameGen
{
	public class MainClass
	{
		// variable definitions

		public static readonly Random random = new Random();

		// application logic begins here

		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Application.Init();
				var win = new MainWindow();
				win.Show();
				Application.Run();
			}

			else if (args[0] == "-i" || args[0] == "--interactive")
			{
				Application.Init();
				var win = new MainWindow();
				win.Show();
				Application.Run();
			}

			else if (args.Length >= 3 && (args[0] == "-n" || args[0] == "--non-interactive"))
			{

				if (!uint.TryParse(args[2], out uint inputInt))
				{
					Console.WriteLine("A positive integer was not specified for `number'.");
					Environment.Exit(1);
				}
                uint inputInt2;
                if (args[3] == null)
                {
                    inputInt2 = 48;
                }
                else
                {
					if (!uint.TryParse(args[3], out inputInt2))
					{
						Console.WriteLine("A positive integer was not specified for `buffsize'.");
						Environment.Exit(1);
					}
                }
				Console.WriteLine("KSPNameGen v0.1.2");
				if (NameGen.validParams.Contains(args[1]))
				{
					NameGen.Iterator(inputInt, args[1], inputInt2);
				}
				else
				{
					Console.WriteLine("Specified type is invalid.");
					Environment.Exit(1);
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
			Environment.Exit(0);
		}
		public static void Usage(bool error)
		{
			Console.Write("Usage: KSPNameGen.exe [-i|--interactive] [-n|--non-interactive parameter number [buffsize]] [-h|--help]\n\n" +
			"-i, --interactive: interactive mode (default option if no parameter specified)\n" +
			"-n, --non-interactive: non-interactive mode\n" +
			"-h, --help: show this help\n" +
			"parameter: either of [f|s] [r|c|p] [m|f] in this order. Run in interactive mode to learn more.\n" +
			"number: how many names to generate at once.\n" +
			"buffsize: the size of the buffer (i.e. how many names to write to stdout at once).\n" +
			"number and buffsize must be nonzero integers less than 4294967295 (2^32-1).\n" +
			"`buffsize' is optional; if not given, the default is 48.\n" +
			"`parameter', `number', and `buffsize' are only used with non-interactive mode.\n\n");
			if (error)
			{
				Environment.Exit(1);
			}
			else
			{
				Environment.Exit(0);
			}
		}
	}
}