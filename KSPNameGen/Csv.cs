//Csv.cs
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

//using directives

using System;
using System.Collections.Generic;

namespace KSPNameGen
{
	class Csv
	{
		int order; //0 = flat string
		string name;
		Csv[] members;
		string[] memberNames;
		int[] memberOrders;
		
		public Csv(string content)
		{
			name = content;
			order = 0;
			memberOrders = new int[1]{0};
		}
		
		public Csv(string[] content)
		{
			name = content[0];
			order = 1;
			memberNames = content;
			members = new Csv[content.Length];
			memberOrders = new int[content.Length];
			for(int i = 0; i < content.Length; i++)
			{
				members[i] = new Csv(content[i]);
				memberOrders[i] = 0;
			}
		}
		
		public Csv(Csv[] content)
		{
			name = content[0].GetName();
			order = 2;
			members = content;
			memberNames = new string[content.Length];
			memberOrders = new int[content.Length];
			for(int i = 0; i < content.Length; i++)
			{
				memberNames[i] = content[i].GetName();
				memberOrders[i] = content[i].GetOrder();
				order = content[i].GetOrder() > order ? content[i].GetOrder() : order;
			}
		}
		
		public string GetName()
		{
			return name;
		}
		
		public string[] GetMemberNames()
		{
			return memberNames;
		}
		
		public Csv[] GetMembers()
		{
			if(order == 0)
			{
				return new Csv[1]{this};
			}
			return members;
		}
		
		public bool IsString()
		{
			return order == 0;
		}
		
		public bool IsFlat()
		{
			return order <= 1;
		}
		
		public bool IsDeep()
		{
			return order >= 2;
		}
		
		public int GetOrder()
		{
			return order;
		}
	}
}
