// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PhotoPicker
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem BtnEdit { get; set; }

		[Outlet]
		UIKit.UIImageView ImgBottom { get; set; }

		[Outlet]
		UIKit.UIImageView ImgProfile { get; set; }

		[Outlet]
		UIKit.UILabel LblBottom { get; set; }

		[Outlet]
		UIKit.UILabel LblProfile { get; set; }

		[Outlet]
		UIKit.UIView ViewBottom { get; set; }

		[Outlet]
		UIKit.UIView ViewProfile { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImgProfile != null) {
				ImgProfile.Dispose ();
				ImgProfile = null;
			}

			if (ImgBottom != null) {
				ImgBottom.Dispose ();
				ImgBottom = null;
			}

			if (LblProfile != null) {
				LblProfile.Dispose ();
				LblProfile = null;
			}

			if (LblBottom != null) {
				LblBottom.Dispose ();
				LblBottom = null;
			}

			if (BtnEdit != null) {
				BtnEdit.Dispose ();
				BtnEdit = null;
			}

			if (ViewProfile != null) {
				ViewProfile.Dispose ();
				ViewProfile = null;
			}

			if (ViewBottom != null) {
				ViewBottom.Dispose ();
				ViewBottom = null;
			}
		}
	}
}
