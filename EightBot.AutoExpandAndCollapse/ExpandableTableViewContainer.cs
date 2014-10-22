using System;
using MonoTouch.UIKit;

namespace EightBot.AutoExpandAndCollapse
{
	public class ExpandableTableViewContainer : UITableViewController
	{
		public ExpandableTableViewContainer ()
		{
			Title = "Expandable Cells";

			TableView.Source = new ExpandableSource ();
		}
	}
}

