using System;
using System.Linq;
using Foundation;
using UIKit;

namespace Naxam.LottieDemo.iOS
{
	public partial class JSONExplorerViewController : UITableViewController
	{
		private string[] animations;
		public event EventHandler<string> AnamiationSelected;

		protected JSONExplorerViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			TableView.RegisterClassForCellReuse(typeof(UITableViewCell), "cell");
			btnClose.Clicked += BtnClose_Clicked;

			animations = NSBundle.MainBundle.PathsForResources("json", "TypeFace")
			                     .Union(NSBundle.MainBundle.PathsForResources("json"))
			                     .Select(x => x.Split('/').Last())
			                     .OrderBy(x => x)
			                     .ToArray();
			
			TableView.ReloadData();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return animations?.Length ?? 0;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("cell");

			cell.TextLabel.Text = animations[indexPath.Row];

			return cell;
		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			AnamiationSelected?.Invoke(this, animations[indexPath.Row]);
			DismissViewController(true, null);
		}

		void BtnClose_Clicked(object sender, EventArgs e)
		{
			DismissViewController(true, null);
		}
	}
}

