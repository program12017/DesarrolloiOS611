// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ExamenTwitter
{
	[Register ("TweetTableViewCell")]
	partial class TweetTableViewCell
	{
		[Outlet]
		UIKit.UIImageView ImgUser { get; set; }

		[Outlet]
		UIKit.UILabel LblFavoriteCount { get; set; }

		[Outlet]
		UIKit.UILabel LblName { get; set; }

		[Outlet]
		UIKit.UILabel LblRetweetCount { get; set; }

		[Outlet]
		UIKit.UITextView TxtDescription { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImgUser != null) {
				ImgUser.Dispose ();
				ImgUser = null;
			}

			if (LblName != null) {
				LblName.Dispose ();
				LblName = null;
			}

			if (LblFavoriteCount != null) {
				LblFavoriteCount.Dispose ();
				LblFavoriteCount = null;
			}

			if (LblRetweetCount != null) {
				LblRetweetCount.Dispose ();
				LblRetweetCount = null;
			}

			if (TxtDescription != null) {
				TxtDescription.Dispose ();
				TxtDescription = null;
			}
		}
	}
}
