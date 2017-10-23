// Menu.cs
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
using System.Collections.Generic;

namespace KSPNameGen
{
	class Menu
	{
		List<Line> content;
		int cursor;
		static ConsoleColor[] highlights = new ConsoleColor[4]{
			Console.BackgroundColor,
			ConsoleColor.DarkRed,
			ConsoleColor.Green,
			ConsoleColor.Red};
		List<int> def;
		
		public int[] indices
		{
			get
			{
				int[] output = new int[content.Count];
				for(int i = 0; i < output.Length; i++)
				{
					output[i] = content[i].index;
				}
				return output;
			}
		}
		
		public string[] selections
		{
			get
			{
				string[] output = new string[content.Count];
				for(int i = 0; i < output.Length; i++)
				{
					output[i] = content[i].GetSelection();
				}
				return output;
			}
		}
		
		public string[] displays
		{
			get
			{
				string[] output = new string[content.Count];
				{
					for(int i = 0; i < output.Length; i++)
					{
						output[i] = content[i].display;
					}
				}
				return output;
			}
		}
		
		public Menu(Line l)
		{
			cursor = 0;
			content = new List<Line>();
			content.Add(l);
			def.Add(0);
		}
		
		public Menu(Line l, int d)
		{
			cursor = 0;
			content = new List<Line>();
			content.Add(l);
			def.Add(d);
		}
		
		public Menu(Line[] l, int[] d)
		{
			cursor = 0;
			content = new List<Line>();
			content.AddRange(l);
			def.AddRange(d);
		}
		
		public void AddLine(Line l)
		{
			content.Add(l);
			def.Add(0);
		}
		
		/*
		void UpdateDisplays()
		{
			string[] output = new string[content.Count];
			{
				for(int i = 0; i < output.Length; i++)
				{
					output[i] = content[i].display;
				}
			}
			displays = output;
		}
		*/
		
		public void Draw()
		{
			Console.Clear();
			for(int i = 0; i < content.Count; i++)
			{
				Console.BackgroundColor = cursor == i ? highlights[1] : highlights[def[i]];	//indicate selection
				Console.WriteLine(displays[i]);
				Console.BackgroundColor = highlights[0];
			}
		}
		
		public void Draw(string message)
		{
			Draw();
			Console.WriteLine(message);
		}
		
		public void MoveCursor(bool isLeft)
		{
			content[cursor].MoveCursor(isLeft);
		}
	}
}
//










