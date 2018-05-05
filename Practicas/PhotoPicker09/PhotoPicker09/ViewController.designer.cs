// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PhotoPicker09
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem BtnEdit { get; set; }

		[Outlet]
		UIKit.UIView CoverView { get; set; }

		[Outlet]
		UIKit.UIImageView ImgCover { get; set; }

		[Outlet]
		UIKit.UIImageView ImgProfile { get; set; }

		[Outlet]
		UIKit.UILabel LblCover { get; set; }

		[Outlet]
		UIKit.UILabel lblEdit { get; set; }

		[Outlet]
		UIKit.UIView ProfileView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnEdit != null) {
				BtnEdit.Dispose ();
				BtnEdit = null;
			}

			if (CoverView != null) {
				CoverView.Dispose ();
				CoverView = null;
			}

			if (ImgCover != null) {
				ImgCover.Dispose ();
				ImgCover = null;
			}

			if (ImgProfile != null) {
				ImgProfile.Dispose ();
				ImgProfile = null;
			}

			if (LblCover != null) {
				LblCover.Dispose ();
				LblCover = null;
			}

			if (lblEdit != null) {
				lblEdit.Dispose ();
				lblEdit = null;
			}

			if (ProfileView != null) {
				ProfileView.Dispose ();
				ProfileView = null;
			}
		}
	}
}
