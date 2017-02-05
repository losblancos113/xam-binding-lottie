// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Naxam.LottieDemo
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnReplay { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView lstSamples { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vwLogo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnReplay != null) {
                btnReplay.Dispose ();
                btnReplay = null;
            }

            if (lstSamples != null) {
                lstSamples.Dispose ();
                lstSamples = null;
            }

            if (vwLogo != null) {
                vwLogo.Dispose ();
                vwLogo = null;
            }
        }
    }
}