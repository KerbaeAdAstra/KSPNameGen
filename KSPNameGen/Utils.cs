// Utils.cs
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

// warning suppression declarations

#pragma warning disable CS1717

namespace KSPNameGen
{
	public static class Utils
	{
		public static bool FlagExists(string[] arr, string flag)
		{
			return Array.Exists(arr, element => element == flag);
		}

		public static bool FlagParse(string[] arr, string flag, out ulong product, ulong def)
		{
			product = def;

			if (!Array.Exists(arr, element => element == flag))
			{
				return false;
			}
			int index = Array.IndexOf(arr, flag);
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

		public static bool FlagParse(string[] arr, string flag, out string product, string def)
		{
			product = def;

			if (!Array.Exists(arr, element => element == flag))
			{

				product = product;
				return false;
			}
			int index = Array.IndexOf(arr, flag);
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
	}
}
