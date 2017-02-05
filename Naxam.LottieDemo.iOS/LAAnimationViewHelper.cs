using System;
using Foundation;
using Lottie;

namespace Naxam.LottieDemo.iOS
{
	public static class LAAnimationViewHelper
	{
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
