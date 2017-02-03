using System;
using Foundation;
using UIKit;

namespace Naxam.LottieDemo
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			//var resPath = NSBundle.MainBundle.PathForResource("LottieLogo1", "json");
			//var data = NSData.FromFile(resPath);
			//NSError error;
			//var json = NSJsonSerialization.Deserialize(data, 0, out error) as NSDictionary;

			//var lottieLogo = Lottie.LAAnimationView.AnimationFromJSON(json);

			var lottieLogo = Lottie.LAAnimationView.AnimationNamed("LottieLogo1");
			lottieLogo.ContentMode = UIViewContentMode.ScaleAspectFill;

			View.AddSubview(lottieLogo);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
