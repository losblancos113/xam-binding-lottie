using System;
using CoreGraphics;
using Foundation;
using Lottie;
using UIKit;

namespace Naxam.LottieDemo.iOS
{
	public class AnimationTransitionViewController : UIViewController, IUIViewControllerTransitioningDelegate
	{
		private UIButton btnNavigate;
		private UIButton btnClose;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnClose = new UIButton(UIButtonType.Custom);
			btnClose.SetTitle("Close", UIControlState.Normal);
			btnClose.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnClose.BackgroundColor = LAAnimationViewHelper.LottieColor;
			btnClose.Layer.CornerRadius = 4;
			btnClose.TouchUpInside += delegate
			{
				DismissViewController(true, null);
			};

			btnNavigate = new UIButton(UIButtonType.Custom);
			btnNavigate.SetTitle("Show Transition A", UIControlState.Normal);
			btnNavigate.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnNavigate.BackgroundColor = LAAnimationViewHelper.LottieColor;
			btnNavigate.Layer.CornerRadius = 4;
			btnNavigate.TouchUpInside += BtnNavigate_TouchUpInside;

			View.AddSubviews(btnNavigate, btnClose);

			View.BackgroundColor = LAAnimationViewHelper.LottieColorPurple;
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			var rootSize = View.Bounds.Size;

			var btnNavSize = btnNavigate.SizeThatFits(rootSize);
			btnNavSize.Height += 20;
			btnNavSize.Width += 20;

			btnNavigate.Frame = new CGRect(0, 0, btnNavSize.Width, btnNavSize.Height);
			btnNavigate.Center = View.Center;

			var btnCloseSize = btnClose.SizeThatFits(rootSize);
			btnCloseSize.Height += 20;
			btnCloseSize.Width += 20;

			btnClose.Frame = new CGRect(0, 0, btnCloseSize.Width, btnCloseSize.Height);
			btnClose.Center = new CGPoint(View.Center.X, View.Bounds.GetMaxY() - btnCloseSize.Height);
		}

		void BtnNavigate_TouchUpInside(object sender, EventArgs e)
		{
			ToAnimationViewController vc = new ToAnimationViewController();
			vc.TransitioningDelegate = this;

			PresentViewController(vc, true, null);
		}

		[Export("animationControllerForPresentedController:presentingController:sourceController:")]
		public IUIViewControllerAnimatedTransitioning AnimationControllerForPresentedController(
			UIViewController presented,
			UIViewController presenting,
			UIViewController source)
		{
			return new LAAnimationTransitionController("vcTransition1", "outLayer", "inLayer");
		}

		[Export("animationControllerForDismissedController:")]
		public IUIViewControllerAnimatedTransitioning AnimationControllerForDismissedController(UIViewController dismissed)
		{
			return new LAAnimationTransitionController("vcTransition2", "outLayer", "inLayer");
		}
	}

	public class ToAnimationViewController : UIViewController
	{
		private UIButton btnNavigate;
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnNavigate = new UIButton(UIButtonType.Custom);
			btnNavigate.SetTitle("Show Transition B", UIControlState.Normal);
			btnNavigate.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnNavigate.BackgroundColor = LAAnimationViewHelper.LottieColor;
			btnNavigate.Layer.CornerRadius = 4;
			btnNavigate.TouchUpInside += BtnNavigate_TouchUpInside;

			View.BackgroundColor = LAAnimationViewHelper.LottieColorPurple;
			View.AddSubview(btnNavigate);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			var rootSize = View.Bounds.Size;

			var btnNavSize = btnNavigate.SizeThatFits(rootSize);
			btnNavSize.Height += 20;
			btnNavSize.Width += 20;

			btnNavigate.Frame = new CGRect(0, 0, btnNavSize.Width, btnNavSize.Height);
			btnNavigate.Center = View.Center;
		}

		void BtnNavigate_TouchUpInside(object sender, EventArgs e)
		{
			PresentingViewController.DismissViewController(true, null);
		}
	}
}
