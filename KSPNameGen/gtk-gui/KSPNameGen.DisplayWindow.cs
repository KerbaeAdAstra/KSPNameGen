
// This file has been generated by the GUI designer. Do not modify.
namespace KSPNameGen
{
	public partial class DisplayWindow
	{
		private global::Gtk.ScrolledWindow ngScrolledWindow;

		private global::Gtk.TextView ngTextView;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget KSPNameGen.DisplayWindow
			this.Name = "KSPNameGen.DisplayWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("DisplayWindow");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child KSPNameGen.DisplayWindow.Gtk.Container+ContainerChild
			this.ngScrolledWindow = new global::Gtk.ScrolledWindow();
			this.ngScrolledWindow.CanFocus = true;
			this.ngScrolledWindow.Name = "ngScrolledWindow";
			this.ngScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child ngScrolledWindow.Gtk.Container+ContainerChild
			this.ngTextView = new global::Gtk.TextView();
			this.ngTextView.CanFocus = true;
			this.ngTextView.Name = "ngTextView";
			this.ngTextView.Editable = false;
			this.ngTextView.AcceptsTab = false;
			this.ngScrolledWindow.Add(this.ngTextView);
			this.Add(this.ngScrolledWindow);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
		}
	}
}
