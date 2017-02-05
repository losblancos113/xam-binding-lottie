using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Lottie;

namespace Naxam.LottieDemo.Droid
{
	[Activity(Label = "LottieDemo", MainLauncher = true, Icon = "@mipmap/ic_launcher", Theme="@style/AppTheme")]
	public class MainActivity : AppCompatActivity
	{
		LottieAnimationView animationView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_view);
		}

		protected override void OnStart()
		{
			base.OnStart();

			animationView.Progress = 0;
			animationView.PlayAnimation();
		}

		protected override void OnStop()
		{
			base.OnStop();
			animationView.CancelAnimation();
		}
	}
}

