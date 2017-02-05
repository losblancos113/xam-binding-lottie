using System;
using System.Linq;
using Foundation;
using Lottie;
using UIKit;

namespace Naxam.LottieDemo.iOS
{
	public partial class AnimationExplorerViewController : UIViewController
	{
		private LAAnimationView vwAnimation;

		protected AnimationExplorerViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			sldChangeProgress.MinValue = 0;
			sldChangeProgress.MaxValue = 1;

			sldChangeProgress.ValueChanged += AnimationProgressChanged;

			btnSelectAnimation.Clicked += SelectAnimation;
			btnOpenAnimationFromUrl.Clicked += EnterUrl;
			btnToggleLoop.Clicked += ToggleLoop;
			btnPlayAnimation.Clicked += PlayAnimation;
			btnChangeMode.Clicked += ChangeMode;
			btnChangeBackground.Clicked += ChangeBg;

			btnClose.Clicked += Close;

			View.BackgroundColor = UIColor.White;

			ResetAll();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			if (segue.Identifier != "JSONExplorerViewController") {
				return;
			}

			var nav = segue.DestinationViewController as UINavigationController;
			var dest = nav?.TopViewController as JSONExplorerViewController;
			if (dest == null) {
				return;
			}

			dest.AnamiationSelected -= AnamiationSelected;
			dest.AnamiationSelected += AnamiationSelected;
		}

		void AnimationProgressChanged(object sender, EventArgs e)
		{
			if (vwAnimation == null)
			{
				return;
			}

			vwAnimation.AnimationProgress = sldChangeProgress.Value;
		}

		void EnterUrl(object sender, EventArgs e)
		{
			var alertViewController = UIAlertController.Create("Load from URL", null, UIAlertControllerStyle.Alert);
			alertViewController.AddTextField(txtUrl =>
			{
				txtUrl.Placeholder = "Enter URL";
			});

			var loadAction = UIAlertAction.Create("Load", UIAlertActionStyle.Default, action => {
				UrlEntered(alertViewController.TextFields.First().Text);
			});
			var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);

			alertViewController.AddAction(loadAction);
			alertViewController.AddAction(cancelAction);

			PresentViewController(alertViewController, true, null);
		}

		void UrlEntered(string url) {
			try
			{
				var xurl = NSUrl.FromString(url);

				vwAnimation?.RemoveFromSuperview();
				ResetAll();

				vwAnimation = new LAAnimationView(xurl);
				vwAnimation.ContentMode = UIViewContentMode.ScaleAspectFit;

				vwLottieContainer.AddSubview(vwAnimation);
				View.SetNeedsLayout();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		void SelectAnimation(object sender, EventArgs e)
		{
			PerformSegue("JSONExplorerViewController", this);
		}

		void ChangeMode(object sender, EventArgs e)
		{
			if (vwAnimation == null) {
				return;
			}

			switch (vwAnimation.ContentMode)
			{
				case UIViewContentMode.ScaleAspectFill:
					vwAnimation.ContentMode = UIViewContentMode.ScaleToFill;
					break;
				case UIViewContentMode.ScaleAspectFit:
					vwAnimation.ContentMode = UIViewContentMode.ScaleAspectFill;
					break;
				case UIViewContentMode.ScaleToFill:
					vwAnimation.ContentMode = UIViewContentMode.ScaleAspectFit;
					break;
			}
		}

		void ChangeBg(object sender, EventArgs e)
		{
			if (View.BackgroundColor == UIColor.White)
			{
				View.BackgroundColor = UIColor.Black;
			}
			else if (View.BackgroundColor == UIColor.Black)
			{
				View.BackgroundColor = LAAnimationViewHelper.LottieColor;
			} else {
				View.BackgroundColor = UIColor.White;
			}
		}

		void Close(object sender, EventArgs e)
		{
			DismissViewController(true, null);
		}

		void PlayAnimation(object sender, EventArgs e) {
			if (vwAnimation == null) {
				return;
			}

			if (vwAnimation.IsAnimationPlaying) { 
				ResetButton(btnPlayAnimation, false);
				vwAnimation.Pause();
				return;
			}

			ResetButton(btnPlayAnimation, true);
			vwAnimation.AnimationProgress = 0;
			vwAnimation.PlayWithCompletion(PlayCompleted);
		}

		void PlayCompleted(bool completed) {
			sldChangeProgress.Value = (float)vwAnimation.AnimationProgress;
			ResetButton(btnPlayAnimation, false);
		}

		void ToggleLoop(object sender, EventArgs e) {
			if (vwAnimation == null) {
				return;
			}

			vwAnimation.LoopAnimation = !vwAnimation.LoopAnimation;
			ResetButton(btnToggleLoop, vwAnimation.LoopAnimation);
		}

		void AnamiationSelected(object sender, string animation)
		{
			vwAnimation?.RemoveFromSuperview();
			ResetAll();

			vwAnimation = LAAnimationViewHelper.AnimationFromJsonFileInSubfolder(animation, "TypeFace");
			vwAnimation.ContentMode = UIViewContentMode.ScaleAspectFit;

			vwLottieContainer.AddSubview(vwAnimation);
			View.SetNeedsLayout();
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (vwAnimation == null) {
				return;
			}

			vwAnimation.Frame = vwLottieContainer.Bounds;
			vwAnimation.Play();
		}

		void ResetAll() {
			sldChangeProgress.Value = 0;

			foreach (var item in toolbar.Items)
			{
				ResetButton(item, false);
			}
		}

		void ResetButton(UIBarButtonItem item, bool highlighted) {
			item.TintColor = highlighted
				? UIColor.Red
				: LAAnimationViewHelper.LottieColor;
		}
	}
}

