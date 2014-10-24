using System;
using MonoTouch.UIKit;
using Cirrious.FluentLayouts.Touch;
using System.Linq;
using MonoTouch.Foundation;

namespace EightBot.AutoExpandAndCollapse
{
	public class ScrollViewExpandAndCollapse : UIViewController
	{

		UIScrollView scrollView;

		UIButton expand1, expand2;

		UITableView table1, table2;

		FluentLayout tableHeight1, tableHeight2;

		bool table1Expanded, table2Expanded;

		public ScrollViewExpandAndCollapse ()
		{
			View = scrollView = 
				new UIScrollView (View.Bounds);

			View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
				
			View.BackgroundColor = UIColor.White;

			expand1 = new UIButton (UIButtonType.System);

			expand1.TouchUpInside += (object sender, EventArgs e) => {

				table1Expanded = !table1Expanded;

				var height =
					Enumerable
						.Range(0, table1.NumberOfRowsInSection(0))
						.Select(position => {
							return table1.RectForRowAtIndexPath(NSIndexPath.FromItemSection(position, 0)).Height;
						})
						.Aggregate((aggregated, nextValue) => aggregated += nextValue);

				tableHeight1.Minus(tableHeight1.Constant);

				tableHeight1.Plus(table1Expanded ? height : 0f);

				this.UpdateViewConstraints();

				expand1.SetTitle(table1Expanded ? "Collapse" : "Embiggen'", UIControlState.Normal);

				UIView.Animate (.2d, View.LayoutIfNeeded);
			};
			expand1.SetTitle ("Embiggen'", UIControlState.Normal);
			View.Add (expand1);

			table1 = new UITableView ();
			table1.BackgroundColor = UIColor.Red;
			table1.Source = new ExpandableSource ();
			View.Add (table1);

			expand2 = new UIButton (UIButtonType.System);

			expand2.TouchUpInside += (object sender, EventArgs e) => {

				table2Expanded = !table2Expanded;

				tableHeight2.Minus(tableHeight2.Constant);

				var height =
					Enumerable
						.Range(0, table2.NumberOfRowsInSection(0))
						.Select(position => {
							return table2.RectForRowAtIndexPath(NSIndexPath.FromItemSection(position, 0)).Height;
						})
						.Aggregate((aggregated, nextValue) => aggregated += nextValue);

				tableHeight2.Plus(table2Expanded ? height : 0f);

				this.UpdateViewConstraints();

				expand2.SetTitle(table2Expanded ? "Collapse" : "Embiggen'", UIControlState.Normal);

				UIView.Animate (.6d, View.LayoutIfNeeded);
			};
			expand2.SetTitle ("Embiggen'", UIControlState.Normal);
			View.Add (expand2);

			table2 = new UITableView ();
			table2.BackgroundColor = UIColor.Red;
			table2.Source = new ExpandableSource ();
			View.Add (table2);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints ();

			tableHeight1 = new FluentLayout (table1, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 0f);

			tableHeight2 = new FluentLayout (table2, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 
				Enumerable
					.Range(0, table2.NumberOfRowsInSection(0))
					.Select(position => {
						return table2.RectForRowAtIndexPath(NSIndexPath.FromItemSection(position, 0)).Height;
					})
					.Aggregate((aggregated, nextValue) => aggregated += nextValue)
			);

			table2Expanded = true;

			UpdateViewConstraints ();
		}
			
		public override void UpdateViewConstraints ()
		{
			View.RemoveConstraints (View.Constraints);

			View.AddConstraints (
				expand1.AtTopOf(View),
				expand1.AtLeftOf(View),
				expand1.WithSameWidth(View),

				table1.Below(expand1),
				table1.AtLeftOf(View),
				table1.WithSameWidth(View),

				expand2.Below(table1),
				expand2.AtLeftOf(View),
				expand2.WithSameWidth(View),

				table2.Below(expand2),
				table2.AtLeftOf(View),
				table2.WithSameWidth(View),
				table2.AtBottomOf(View),

				tableHeight1,
				tableHeight2
			);


			base.UpdateViewConstraints ();
		}
	}
}

