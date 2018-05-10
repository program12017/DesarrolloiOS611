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
	[Register ("CredentialsViewController")]
	partial class CredentialsViewController
	{
		[Outlet]
		UIKit.UITextField TxtAccessToken { get; set; }

		[Outlet]
		UIKit.UITextField TxtAccessTokenSecret { get; set; }

		[Outlet]
		UIKit.UITextField TxtConsumerKey { get; set; }

		[Outlet]
		UIKit.UITextField TxtConsumerKeySecret { get; set; }

		[Outlet]
		UIKit.UITextField TxtScreenName { get; set; }

		[Outlet]
		UIKit.UITextField TxtUserID { get; set; }

		[Action ("BtnIrABusqueda_TouchUpInside:")]
		partial void BtnIrABusqueda_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (TxtAccessToken != null) {
				TxtAccessToken.Dispose ();
				TxtAccessToken = null;
			}

			if (TxtAccessTokenSecret != null) {
				TxtAccessTokenSecret.Dispose ();
				TxtAccessTokenSecret = null;
			}

			if (TxtConsumerKey != null) {
				TxtConsumerKey.Dispose ();
				TxtConsumerKey = null;
			}

			if (TxtConsumerKeySecret != null) {
				TxtConsumerKeySecret.Dispose ();
				TxtConsumerKeySecret = null;
			}

			if (TxtUserID != null) {
				TxtUserID.Dispose ();
				TxtUserID = null;
			}

			if (TxtScreenName != null) {
				TxtScreenName.Dispose ();
				TxtScreenName = null;
			}
		}
	}
}
