// This file has been autogenerated from a class added in the UI designer.

using System;
using ExamenTwitter.Models;
using Foundation;
using UIKit;

namespace ExamenTwitter
{
	public partial class CredentialsViewController : UIViewController
	{
        #region Constructors

        public CredentialsViewController(IntPtr handle) : base(handle)
        {
        }

        #endregion

        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        #endregion

        #region User Interactions

        partial void BtnIrABusqueda_TouchUpInside(NSObject sender)
        {
            if (TxtAccessToken.Text != "" && TxtAccessTokenSecret.Text != "" &&
                TxtConsumerKey.Text != "" && TxtConsumerKeySecret.Text != "" &&
                TxtUserID.Text != "" && TxtScreenName.Text != "")
            {
                ulong userID;

                if (ulong.TryParse(TxtUserID.Text, out userID))
                {
                    try
                    {
                        TwitterModel modeloTwitter = new TwitterModel(TxtConsumerKey.Text, TxtConsumerKeySecret.Text, TxtAccessToken.Text, TxtAccessTokenSecret.Text, TxtScreenName.Text, userID);


                        var viewController = Storyboard.InstantiateViewController(nameof(TwitterTableViewController)) as TwitterTableViewController;
                        viewController.ModeloTwitter = modeloTwitter;

                        NavigationController.PushViewController(viewController, true);
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("ERROR:", $"Hubo un error en la inserción de los datos, verifíquelos: {ex.Message}");
                    }

                }
                else
                {
                    ShowMessage("ERROR:", "En el ID sólo puede haber números.");
                }


            }
            else
            {
                ShowMessage("ADVERTENCIA:", "Llene todos los campos, por favor.");
            }

        }

        #endregion


        #region Internal Functionality

        public void ShowMessage(string title, string content)
        {
            UIAlertController alert = UIAlertController.Create(title, content, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            PresentViewController(viewControllerToPresent: alert, animated: true, completionHandler: null);
        }

        #endregion
    }
}
