// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ExamenPrimerParcial
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton BtnCalculate { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem BtnGoToSettings { get; set; }

		[Outlet]
		UIKit.UILabel LblAddition { get; set; }

		[Outlet]
		UIKit.UILabel LblDivision { get; set; }

		[Outlet]
		UIKit.UILabel LblMultiplication { get; set; }

		[Outlet]
		UIKit.UILabel LblNumber1 { get; set; }

		[Outlet]
		UIKit.UILabel LblNumber2 { get; set; }

		[Outlet]
		UIKit.UILabel LblResult { get; set; }

		[Outlet]
		UIKit.UILabel LblSubstraction { get; set; }

		[Outlet]
		UIKit.UISlider SldOperator { get; set; }

		[Outlet]
		UIKit.UITextField TxtNumber1 { get; set; }

		[Outlet]
		UIKit.UITextField TxtNumber2 { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnCalculate != null) {
				BtnCalculate.Dispose ();
				BtnCalculate = null;
			}

			if (LblAddition != null) {
				LblAddition.Dispose ();
				LblAddition = null;
			}

			if (LblDivision != null) {
				LblDivision.Dispose ();
				LblDivision = null;
			}

			if (LblMultiplication != null) {
				LblMultiplication.Dispose ();
				LblMultiplication = null;
			}

			if (LblNumber1 != null) {
				LblNumber1.Dispose ();
				LblNumber1 = null;
			}

			if (LblNumber2 != null) {
				LblNumber2.Dispose ();
				LblNumber2 = null;
			}

			if (LblResult != null) {
				LblResult.Dispose ();
				LblResult = null;
			}

			if (LblSubstraction != null) {
				LblSubstraction.Dispose ();
				LblSubstraction = null;
			}

			if (SldOperator != null) {
				SldOperator.Dispose ();
				SldOperator = null;
			}

			if (TxtNumber1 != null) {
				TxtNumber1.Dispose ();
				TxtNumber1 = null;
			}

			if (TxtNumber2 != null) {
				TxtNumber2.Dispose ();
				TxtNumber2 = null;
			}

			if (BtnGoToSettings != null) {
				BtnGoToSettings.Dispose ();
				BtnGoToSettings = null;
			}
		}
	}
}
