// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PracticaSegue
{
	[Register ("FirstViewController")]
	partial class FirstViewController
	{
		[Outlet]
		UIKit.UIDatePicker DatePickerNacimiento { get; set; }

		[Outlet]
		UIKit.UIPickerView PickerGenero { get; set; }

		[Outlet]
		UIKit.UITextField TxtNombre { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TxtNombre != null) {
				TxtNombre.Dispose ();
				TxtNombre = null;
			}

			if (DatePickerNacimiento != null) {
				DatePickerNacimiento.Dispose ();
				DatePickerNacimiento = null;
			}

			if (PickerGenero != null) {
				PickerGenero.Dispose ();
				PickerGenero = null;
			}
		}
	}
}
