//
// Utils.cs
//
// Author:
//       Kerbae ad Astra <kerbaeadastra@gmail.com>
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
namespace KSPNameGen
{
    public static class Utils
    {
        public static string PromptS(string query, bool help)
        {
            Console.WriteLine(query);
            if (help)
            {
                Console.WriteLine("Type 'help' for help; 'exit' to exit.");
            }
            return Console.ReadLine().ToLower();
        }
        public static ulong PromptI(string query)
        {
            ulong inputInt64 = 0;
            Console.WriteLine(query);
            string input = Console.ReadLine();
            if (!ulong.TryParse(input, out inputInt64))
            {
                Console.WriteLine("A positive integer was not specified.");
            }
            return inputInt64;
        }
        public static void Usage(bool error)
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
                Environment.Exit(1);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
