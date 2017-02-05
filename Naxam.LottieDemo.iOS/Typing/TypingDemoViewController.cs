using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Naxam.LottieDemo.iOS
{
	public class TypingDemoViewController : UIViewController, IUITextFieldDelegate
	{
		AnimatedTextField textField;
		UITextField typingField;
		UISlider fontSlider;
		UIButton closeButton;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			closeButton = new UIButton(UIButtonType.System);
			closeButton.SetTitle("Close", UIControlState.Normal);
			closeButton.TouchUpInside += delegate {
				typingField.ResignFirstResponder();
				PresentingViewController.DismissViewController(true, null);
			};

			fontSlider = new UISlider(CGRect.Empty);
			fontSlider.MinValue = 18;
			fontSlider.MaxValue = 128;
			fontSlider.Value = 36;
			fontSlider.ValueChanged += delegate {
				textField.FontSize = (int)fontSlider.Value;
			};

			textField = new AnimatedTextField(View.Bounds);
			textField.Text = "Start typing";

			typingField = new UITextField(CGRect.Empty);
			typingField.Alpha = 0;
			typingField.Text = textField.Text;
			typingField.Delegate = this;

			View.AddSubviews(closeButton, fontSlider, textField, typingField);

			NSNotificationCenter.DefaultCenter.AddObserver(this, new ObjCRuntime.Selector("keyboardChanged:"), UIKeyboard.WillShowNotification, null);
			NSNotificationCenter.DefaultCenter.AddObserver(this, new ObjCRuntime.Selector("keyboardChanged:"), UIKeyboard.WillHideNotification, null);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			typingField.BecomeFirstResponder();
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			var buttonSize = closeButton.SizeThatFits(View.Bounds.Size);
			closeButton.Frame = new CGRect(16, 32, buttonSize.Width, 48);
			fontSlider.Frame = new CGRect(16, closeButton.Frame.GetMaxY(), View.Bounds.Size.Width - 20, 48);
			textField.Frame = new CGRect(0, fontSlider.Frame.GetMaxY(), View.Bounds.Size.Width, View.Bounds.Size.Height - fontSlider.Frame.GetMaxY());
			typingField.Frame = new CGRect(0, -100, View.Bounds.Size.Width, 24);
		}

		[Export("textField:shouldChangeCharactersInRange:replacementString:")]
		public bool ShouldChangeCharactersInRange(UITextField textField, NSRange range, string replacementString) {
			this.textField.ChangeCharactersInRange(this.textField, range, replacementString);
			return true;
		}

		[Export("keyboardChanged:")]
		public void KeyboardChanged(NSNotification notification) {
			var keyboardFrame = notification.UserInfo[UIKeyboard.FrameEndUserInfoKey] as NSValue;
			textField.ScrollInsets = new UIEdgeInsets(0, 0, keyboardFrame.CGRectValue.Size.Height, 0);
		}
	}
}
