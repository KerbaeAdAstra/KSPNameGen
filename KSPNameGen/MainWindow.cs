//
// MainWindow.cs
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
using Gtk;

public partial class MainWindow : Gtk.Window
{
	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnExit(object sender, EventArgs e)
	{
		Application.Quit();
	}

	protected string[] authors = {
				"0111narwhalz <0111narwhalz@gmail.com>",
				"TotallyNotHuman <tnh-underscore@outlook.com>"
	};

	protected string license = " Copyright (c) 2017 the Kerbae ad Astra group\n" +
		"\n" +
		" Permission is hereby granted, free of charge, to any person obtaining a copy\n" +
		" of this software and associated documentation files (the \"Software\"), to deal\n" +
		" in the Software without restriction, including without limitation the rights\n" +
		" to use, copy, modify, merge, publish, distribute, sublicense, and/or sell\n" +
		" copies of the Software, and to permit persons to whom the Software is\n" +
		" furnished to do so, subject to the following conditions:\n" +
		"\n" +
		" The above copyright notice and this permission notice shall be included in\n" +
		" all copies or substantial portions of the Software.\n" +
		"\n" +
		" THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\n" +
		" IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\n" +
		" FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\n" +
		" AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\n" +
		" LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\n" +
		" OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN\n" +
		" THE SOFTWARE.";

	protected void OnAbout(object sender, EventArgs e)
	{
		var about = new AboutDialog()
		{
			Authors = authors,
			Copyright = "Copyright (c) 2017 the Kerbae ad Astra group.",
			License = license,
			ProgramName = "KSPNameGen",
			Version = "0.1.2",
			Website = "kerbaeadastra.github.io"
		};

		about.Run();
		about.Destroy();
	}

	protected void FuturePressedEvent(object o, AccelCanActivateArgs args)
	{
	}

	protected void SpinButtonOutput(object o, OutputArgs args)
	{
	}
}