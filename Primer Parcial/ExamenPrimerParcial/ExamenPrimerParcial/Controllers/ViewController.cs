using System;

using UIKit;
using ExamenPrimerParcial.Models;
using ExamenPrimerParcial.Enums;
using Foundation;

namespace ExamenPrimerParcial
{
    public partial class ViewController : UIViewController
    {

        #region Global variables

        bool isFinishedTheAnimation;

        NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;

        NSObject didChangeNotificationToken;

        #endregion


        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.


            //Adjusting the TextFields for Night Mode.
            TxtNumber1.BackgroundColor = UIColor.Clear;
            TxtNumber2.BackgroundColor = UIColor.Clear;


            //Initialize the global variables.
            isFinishedTheAnimation = true;


            //Adding toolbar to the TextFields.
            AddDoneButtonToKeyboard(false);


            //Methods from the controls.
            BtnCalculate.TouchUpInside += BtnCalculate_TouchUpInside;

            SldOperator.ValueChanged += SldOperator_ValueChanged;

            BtnGoToSettings.Clicked += BtnGoToSettings_Clicked;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //To monitor the values from the Settings Bundle. 
            //After receiving the notification, the SettingsChanged functions is called as a callback. 
            didChangeNotificationToken = NSNotificationCenter.DefaultCenter.AddObserver(NSUserDefaults.DidChangeNotification, SettingsChanged);
        }



        public override void ViewWillDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            //Delete the notification (from the token) from the memory, to prevent memory leaks.
            //The three methods are to ensure to free the memory from that nodification.
            NSNotificationCenter.DefaultCenter.RemoveObserver(didChangeNotificationToken);
            didChangeNotificationToken.Dispose();
            didChangeNotificationToken = null;
        }






        #region User interactions

        void BtnCalculate_TouchUpInside(object sender, EventArgs e)
        {

            if (TxtNumber1.Text != "" && TxtNumber2.Text != "" && isFinishedTheAnimation)
            {
                //The values that are going to be processed by the object. 
                double number1;
                double number2;


                if (double.TryParse(TxtNumber1.Text, out number1) && double.TryParse(TxtNumber2.Text, out number2))
                {
                    ArithmeticModel objArithmetic = new ArithmeticModel(number1, number2, GetOperationType());

                    LblResult.Text = objArithmetic.Result;

                }
                else
                {
                    ShowMessage("Error", "Error en los valores de entrada, verifíquelos.");
                }


            }
            else
            {
                ShowMessage("Error", "Llene todos los campos y/o espere a que la barra de operaciones esté quieta.");
            }


        }


        void SldOperator_ValueChanged(object sender, EventArgs e)
        {
            float sldOperatorValue = SldOperator.Value;

            //To only round when it has decimals.
            if ((sldOperatorValue % 1) != 0)
            {

                float roundendValue = (float)Math.Round(sldOperatorValue);

                //Duration, Delay, Type of Animation, Action to Perform, Action on Completed Animation.
                UIView.Animate(0.5, 0, UIViewAnimationOptions.CurveEaseInOut, () => {

                    SldOperator.SetValue(roundendValue, true);

                    isFinishedTheAnimation = false;
                }, () => {

                    isFinishedTheAnimation = true;
                });

            }


        }


        void BtnGoToSettings_Clicked(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
        }

        void ShowMessage(String title, String content)
        {
            UIAlertController alert = UIAlertController.Create(title, content, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            PresentViewController(alert, animated: true, completionHandler: null);
        }

        #endregion


        #region Notifications

        private void SettingsChanged(NSNotification obj)
        {

            var isNightMode = userDefaults.BoolForKey("night_mode");

            if (isNightMode)
            {

                //Changing the background and StatusBar
                View.BackgroundColor = UIColor.DarkGray;

                //It wasn't necesary anymore when adding the Navigation Controller.
                //UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

                var navigationBar = this.NavigationController.NavigationBar;
                navigationBar.TintColor = UIColor.Black;
                navigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Blue };
                BtnGoToSettings.TintColor = UIColor.Red;


                //Chaning the main controls.

                //The TextFields.
                TxtNumber1.TextColor = UIColor.White;
                TxtNumber2.TextColor = UIColor.White;

                TxtNumber1.Layer.BorderColor = UIColor.White.CGColor;
                TxtNumber1.Layer.BorderWidth = 1f;
                TxtNumber2.Layer.BorderColor = UIColor.White.CGColor;
                TxtNumber2.Layer.BorderWidth = 1f;

                TxtNumber1.KeyboardAppearance = UIKeyboardAppearance.Dark;
                TxtNumber2.KeyboardAppearance = UIKeyboardAppearance.Dark;

                AddDoneButtonToKeyboard(true);

                //Then the other controls.
                LblResult.TextColor = UIColor.White;

                SldOperator.TintColor = UIColor.Red;

                BtnCalculate.SetTitleColor(UIColor.Green, UIControlState.Normal);


                //Changing the miscellaneous controls.
                LblNumber1.TextColor = UIColor.White;
                LblNumber2.TextColor = UIColor.White;

                LblAddition.TextColor = UIColor.White;
                LblSubstraction.TextColor = UIColor.White;
                LblMultiplication.TextColor = UIColor.White;
                LblDivision.TextColor = UIColor.White;





            }
            else
            {
                //Changing the background and StatusBar
                View.BackgroundColor = UIColor.White;

                //It wasn't necesary anymore when adding the Navigation Controller.
                //UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;

                var navigationBar = this.NavigationController.NavigationBar;
                navigationBar.TintColor = View.TintColor;
                navigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black };
                BtnGoToSettings.TintColor = View.TintColor;


                //Chaning the main controls.

                //The TextFields.
                TxtNumber1.TextColor = UIColor.Black;
                TxtNumber2.TextColor = UIColor.Black;

                TxtNumber1.Layer.BorderColor = UIColor.Black.CGColor;
                TxtNumber1.Layer.BorderWidth = 0;
                TxtNumber2.Layer.BorderColor = UIColor.Black.CGColor;
                TxtNumber2.Layer.BorderWidth = 0;

                TxtNumber1.KeyboardAppearance = UIKeyboardAppearance.Default;
                TxtNumber2.KeyboardAppearance = UIKeyboardAppearance.Default;

                AddDoneButtonToKeyboard(false);


                //Then the other controls.
                LblResult.TextColor = UIColor.Black;

                SldOperator.TintColor = View.TintColor;


                BtnCalculate.SetTitleColor(View.TintColor, UIControlState.Normal);


                //Changing the miscellaneous controls.
                LblNumber1.TextColor = UIColor.Black;
                LblNumber2.TextColor = UIColor.Black;

                LblAddition.TextColor = UIColor.Black;
                LblSubstraction.TextColor = UIColor.Black;
                LblMultiplication.TextColor = UIColor.Black;
                LblDivision.TextColor = UIColor.Black;


            }

        }

        #endregion

        #region Internal functionality

        ArithmeticEnum GetOperationType()
        {
            ArithmeticEnum arithmeticEnum = new ArithmeticEnum();

            switch (SldOperator.Value)
            {
                case 0:

                    arithmeticEnum = ArithmeticEnum.Addition;
                    break;

                case 1:

                    arithmeticEnum = ArithmeticEnum.Substraction;
                    break;

                case 2:

                    arithmeticEnum = ArithmeticEnum.Multiplication;
                    break;

                case 3:

                    arithmeticEnum = ArithmeticEnum.Division;
                    break;


                default:

                    arithmeticEnum = ArithmeticEnum.Addition;
                    break;
            }

            return arithmeticEnum;


        }


        void AddDoneButtonToKeyboard(bool isNightMode)
        {
            var toolbar = new UIToolbar { BarStyle = UIBarStyle.Default };

            //Adjust the size of the Toolbar automatically.
            toolbar.SizeToFit();

            var btnDone = new UIBarButtonItem(UIBarButtonSystemItem.Done, BtnDone_Clicked);

            //To change according if Night Mode is enabled or not.
            if (isNightMode) {

                btnDone.TintColor = UIColor.Black;
            }


            var bbs = new UIBarButtonItem[] {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),

                //A callback function.
                btnDone
            };
            toolbar.SetItems(bbs, false);

            //To change according if Night Mode is enabled or not.
            if (isNightMode)
            {
                toolbar.BackgroundColor = UIColor.Black;
            }

            TxtNumber1.InputAccessoryView = toolbar;
            TxtNumber2.InputAccessoryView = toolbar;
        }

        void BtnDone_Clicked(object sender, EventArgs e)
        {
            //End editing.
            View.EndEditing(true);

        }

        #endregion


    }
}
