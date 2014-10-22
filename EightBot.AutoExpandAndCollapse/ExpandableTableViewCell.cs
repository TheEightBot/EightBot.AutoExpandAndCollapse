using System;
using MonoTouch.UIKit;
using Cirrious.FluentLayouts.Touch;

namespace EightBot.AutoExpandAndCollapse
{
	public class ExpandableTableViewCell : UITableViewCell
	{
		public const string Key = "ExpandableTableViewCell";

		public UILabel Label1, Label2;

		public ExpandableTableViewCell () : base(UITableViewCellStyle.Default, Key)
		{
			ContentView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;

			Label1 = new UILabel ();
			Add (Label1);

			Label2 = new UILabel ();
			Add (Label2);

			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints ();

			SetNeedsUpdateConstraints ();
		}

		public override void UpdateConstraints ()
		{
			base.UpdateConstraints ();

			RemoveConstraints (Constraints);

			this.AddConstraints (
				Label1.AtTopOf(ContentView),
				Label1.AtLeftOf(ContentView),
				Label1.AtRightOf(ContentView),

				Label2.Below(Label1),
				Label2.AtLeftOf(ContentView),
				Label2.AtRightOf(ContentView),
				Label2.AtBottomOf(ContentView)
			);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ContentView.SetNeedsLayout ();
			ContentView.LayoutIfNeeded ();
		}
	}
}

