// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Naxam.LottieDemo.iOS
{
    [Register ("AnimationExplorerViewController")]
    partial class AnimationExplorerViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnChangeBackground { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnChangeMode { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnClose { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnOpenAnimationFromUrl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnPlayAnimation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnSelectAnimation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnToggleLoop { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sldChangeProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIToolbar toolbar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vwLottieContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnChangeBackground != null) {
                btnChangeBackground.Dispose ();
                btnChangeBackground = null;
            }

            if (btnChangeMode != null) {
                btnChangeMode.Dispose ();
                btnChangeMode = null;
            }

            if (btnClose != null) {
                btnClose.Dispose ();
                btnClose = null;
            }

            if (btnOpenAnimationFromUrl != null) {
                btnOpenAnimationFromUrl.Dispose ();
                btnOpenAnimationFromUrl = null;
            }

            if (btnPlayAnimation != null) {
                btnPlayAnimation.Dispose ();
                btnPlayAnimation = null;
            }

            if (btnSelectAnimation != null) {
                btnSelectAnimation.Dispose ();
                btnSelectAnimation = null;
            }

            if (btnToggleLoop != null) {
                btnToggleLoop.Dispose ();
                btnToggleLoop = null;
            }

            if (sldChangeProgress != null) {
                sldChangeProgress.Dispose ();
                sldChangeProgress = null;
            }

            if (toolbar != null) {
                toolbar.Dispose ();
                toolbar = null;
            }

            if (vwLottieContainer != null) {
                vwLottieContainer.Dispose ();
                vwLottieContainer = null;
            }
        }
    }
}