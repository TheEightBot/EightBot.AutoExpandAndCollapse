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
			//ContentView.TranslatesAutoresizingMaskIntoConstraints = false;
			ContentView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			ContentView.BackgroundColor = UIColor.Red;

			Label1 = new UILabel ();
			ContentView.Add (Label1);

			Label2 = new UILabel ();
			Label2.Lines = 0;
			Label2.BackgroundColor = UIColor.Yellow;
			ContentView.Add (Label2);

			ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints ();

			SetNeedsUpdateConstraints ();
		}

		public override void UpdateConstraints ()
		{
			Console.WriteLine ("Updating Constraints");

			ContentView.RemoveConstraints (Constraints);

			ContentView.AddConstraints (
				//				ContentView.AtLeftOf(this),
				//				ContentView.AtRightOf(this),
				//				ContentView.AtTopOf(this),
				//				ContentView.AtBottomOf(this),

				Label1.AtTopOf(ContentView),
				Label1.AtLeftOf(ContentView),
				Label1.AtRightOf(ContentView),
				Label1.Height().EqualTo(Label1.Font.LineHeight),

				Label2.Below(Label1),
				Label2.AtLeftOf(ContentView),
				Label2.AtRightOf(ContentView),
				Label2.AtBottomOf(ContentView)


			);

			base.UpdateConstraints ();
		}
//
		public override void LayoutSubviews ()
		{
			Console.WriteLine ("Laying out Subviews");

			ContentView.SetNeedsLayout ();
			ContentView.LayoutIfNeeded ();

			base.LayoutSubviews ();
		}
	}
}

