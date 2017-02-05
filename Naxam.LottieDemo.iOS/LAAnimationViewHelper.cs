using System;
using Foundation;
using Lottie;
using UIKit;

namespace Naxam.LottieDemo.iOS
{
	public static class LAAnimationViewHelper
	{
		public static readonly UIColor LottieColor = new UIColor(50.0f / 255.0f, 207.0f / 255.0f, 193.0f / 255.0f, 1f);
		public static readonly UIColor LottieColorPurple = new UIColor(122.0f / 255.0f, 8.0f / 255.0f, 81.0f / 255.0f, 1f);

		public static LAAnimationView AnimationFromJsonFileInSubfolder(string fileName, string subfolder) { 
			var jsonFile = NSBundle.MainBundle.PathForResource(fileName.Split('.')[0], "json");
			if (jsonFile == null)
			{
				jsonFile = NSBundle.MainBundle.PathForResource(fileName.Split('.')[0], "json", subfolder);
			}
			var data = NSData.FromFile(jsonFile);
			NSError error;
			var json = NSJsonSerialization.Deserialize(data, NSJsonReadingOptions.AllowFragments, out error) 
			                              as NSDictionary;

			if (error != null) {
				return null;
			}

			return LAAnimationView.AnimationFromJSON(json);
		}
	}
}
