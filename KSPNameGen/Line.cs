// Line.cs
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
	class Line
	{
		//Represents whether the line is interactive or only informational
		bool isOption
		{
			get;
		}
		Option opt;
		string backing;
		//The string representation of the line
		public string display
		{
			get
			{
				if(!isOption)
				{
					return backing;
				}
				return opt.ToString();
			}
			set
			{
				if(!isOption)
				{
					backing = value;
				}
			}
		}
		
		//If an option, the index of the option. Otherwise, -1.
		public int Index
		{
			get
			{
				if(!isOption)
				{
					return -1;
				}
				return opt.Index;
			}
			set
			{
				opt.Index = value;
			}
		}
		
		//Constructs a Line with an option.
		public Line(Option o)
		{
			isOption = true;
			opt = o;
		}
		
		//Constructs a Line with a simple string.
		public Line(string datum)
		{
			isOption = false;
			backing = datum;
		}
		
		//Returns either the selected option or the display string.
		public string GetSelection()
		{
			if(!isOption)
			{
				return backing;
			}
			return opt.GetSelection();
		}
		
		//If an option, selects the relevant index.
		public void Select(int s)
		{
			if(!isOption)
			{
				return;
			}
			opt.Index = s;
		}
		
		//Moves the cursor
		public void MoveCursor(bool isLeft)
		{
			if(!isOption)
			{
				Console.WriteLine("Not an option!");
				return;
			}
			Index = Index + (isLeft ? -1 : 1);
		}
	}
}
//










