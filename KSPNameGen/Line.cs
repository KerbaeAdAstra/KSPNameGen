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

namespace KSPNameGen
{
	class Line
	{
		bool isOption
		{
			get;
		}
		Option opt;
		public string display;
		
		public int index
		{
			get
			{
				if(!isOption)
				{
					return -1;
				}
				return opt.index;
			}
		}
		
		public Line(Option o)
		{
			isOption = true;
			opt = o;
			display = opt.ToString();
		}
		
		public Line(string datum)
		{
			isOption = false;
			display = datum;
		}
		
		public string GetSelection()
		{
			if(!isOption)
			{
				return display;
			}
			return opt.GetSelection();
		}
		
		public void Select(int s)
		{
			if(!isOption)
			{
				return;
			}
			opt.index = s;
		}
		
		public void MoveCursor(bool isLeft)
		{
			if(!isOption)
			{
				return;
			}
			opt.index = opt.index + (isLeft ? -1 : 1);
		}
	}
}
//










