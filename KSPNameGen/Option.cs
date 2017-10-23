// Option.cs
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

using System;

namespace KSPNameGen
{
	class Option
	{
		string[] options;	//The set of strings representing the options
		int count;				//The quantity of options available
		int index;
		
		//The position of the selected option
		public int Index
		{
			get
			{
				return index;
			}
			
			set
			{
				Console.WriteLine((value + count) % count);
				Console.WriteLine(index);
				index = (value + count) % count;
				Console.WriteLine(index);
			}
		}
		
		//Constructs a new Option with an array of strings
		public Option(string[] choices)
		{
			options = choices;
			count = options.Length;
			Index = 0;
		}
		
		//Turns all options into a single line, surrounding the selected one with square brackets
		public override string ToString()
		{
			string output = "";
			for(int i = 0; i < count; i++)
			{
				output += i == index ? "[" : " ";
				output += options[i];
				output += i == index ? "]" : " ";
			}
			return output;
		}
		
		//Returns the selected option as a string
		public string GetSelection()
		{
			return options[index];
		}
	}
}
//










