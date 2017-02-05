using System;
using CoreGraphics;
using Foundation;
using Naxam.LottieDemo.iOS;
using UIKit;

namespace Naxam.LottieDemo
{
	public partial class ViewController : UIViewController, IUITableViewDelegate, IUITableViewDataSource
	{
		private Lottie.LAAnimationView lottieLogo;
		private Sample[] samples;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			PopulateData();

			lottieLogo = Lottie.LAAnimationView.AnimationNamed("LottieLogo1");
			lottieLogo.ContentMode = UIViewContentMode.ScaleAspectFill;
			vwLogo.InsertSubview(lottieLogo, 0);

			btnReplay.TouchUpInside += BtnReplay_TouchUpInside;

			lstSamples.RegisterClassForCellReuse(typeof(UITableViewCell), "cell");
			lstSamples.Delegate = this;
			lstSamples.DataSource = this;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			lottieLogo.Play();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			lottieLogo.Pause();
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			var rect = new CGRect(0, 0, vwLogo.Bounds.Size.Width, vwLogo.Bounds.Size.Height);
			lottieLogo.Frame = rect;
			//btnReplay.Frame = rect;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		void BtnReplay_TouchUpInside(object sender, EventArgs e)
		{
			lottieLogo.AnimationProgress = 0;
			lottieLogo.Play();
		}

		void PopulateData() { 
			samples = new Sample[] { 
				new Sample { 
					Title = "Animation Explorer",
					ControllerType = typeof(AnimationExplorerViewController)
				},
				new Sample {
					Title = "Animated Keyboard",
					ControllerType = typeof(TypingDemoViewController)
				},
				new Sample {
					Title = "Animated Transitions Demo",
					ControllerType = typeof(AnimationTransitionViewController)
				}
			};
		}

		public nint RowsInSection(UITableView tableView, nint section)
		{
			return samples.Length;
		}

		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var sample = samples[indexPath.Row];
			
			var cell = tableView.DequeueReusableCell("cell");
			cell.TextLabel.Text = sample.Title;

			return cell;
		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var sample = samples[indexPath.Row];

			if (sample.ControllerType == typeof(AnimationExplorerViewController))
			{
				PerformSegue(sample.ControllerType.Name, this);
			}
			else if (sample.ControllerType == typeof(AnimationTransitionViewController))
			{
				PresentViewController(new AnimationTransitionViewController(), true, null);
			}
			else { 
				PresentViewController(new TypingDemoViewController(), true, null);
			}
		}
	}

	public class Sample { 
		public string Title
		{
			get;
			set;
		}

		public Type ControllerType
		{
			get;
			set;
		}
	}
}
