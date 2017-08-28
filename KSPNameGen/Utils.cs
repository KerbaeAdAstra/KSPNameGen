// Utils.cs
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

// using directives

using System;
using System.IO;
using System.Threading;
using static System.Array;
using static System.Console;
using static System.IO.File;

// warning suppression declarations

#pragma warning disable CS1717

namespace KSPNameGen
{
	public static class Utils
	{
		public static bool FlagExists(string[] arr, string flag)
		{
			return Exists(arr, element => element == flag);
		}

		public static bool FlagParse(string[] arr, string flag, out
									 ulong product, ulong def)
		{
			product = def;

			if (!Exists(arr, element => element == flag))
			{
				return false;
			}
			int index = IndexOf(arr, flag);
			if (index == arr.Length - 1)
			{
				return false;
			}
			ulong prod;
			if (!ulong.TryParse(arr[index + 1], out prod))
			{
				return false;
			}
			product = prod;
			return true;
		}

		public static bool FlagParse(string[] arr, string flag, out
									 string product, string def)
		{
			product = def;

			if (!Exists(arr, element => element == flag))
			{

				product = product;
				return false;
			}
			int index = IndexOf(arr, flag);
			if (index == arr.Length - 1)
			{
				return false;
			}
			if (arr[index + 1][0] == '-')
			{
				return false;
			}
			product = arr[index + 1];
			return true;
		}

		public static string PromptS(string query)
		{
			WriteLine(query);
			return ReadLine().ToLower();
		}

		public static ulong PromptI(string query)
		{
			ulong inputLong = 0;
			WriteLine(query);
			if (!ulong.TryParse(ReadLine(), out inputLong))
			{
				WriteLine("A positive nonzero integer was not" +
								  "specified.");
			}
			return inputLong;
		}

		public static string Stringify(int[] _param)
		{
			string output = _param[0] == 0 ? "f" : "s";
			output += _param[1] == 0 ? "p" : _param[1] == 1 ? "r" : "c";
			output += _param[2] == 0 ? "m" : "f";
			return output;
		}

		public static void Kill(ushort exitCode)
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

		public static string GetBasename()
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

		public static bool Accessible(string filePath)
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
	}
}
