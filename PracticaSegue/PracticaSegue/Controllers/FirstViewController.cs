using System;

using UIKit;
using PracticaSegue.Models;
using System.Globalization;

namespace PracticaSegue
{
    public partial class FirstViewController : UIViewController
    {

        #region Class properties

        public string StrGenero { get; set; }

        #endregion


        #region Constructor

        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #endregion

        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Inicialización del PickerView de género.
            //Este DataView guarda constantemente el género en la variable
            //StrGenero.
            PickerGenero.Model = new GeneroPickerViewModel(this);

            //Habilitando la acción de ocultar el teclado al dar Enter.
            TxtNombre.ShouldReturn += TxtNombre_ShouldReturn;



        }
        #endregion


        #region User interactions

        bool TxtNombre_ShouldReturn(UITextField textField)
        {
            View.EndEditing(true);

            return true;
        }

        void ShowMessage(String title, String content)
        {
            UIAlertController alert = UIAlertController.Create(title, content, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            PresentViewController(alert, animated: true, completionHandler: null);
        }

        #endregion



        #region Navigation

        //Atrapar el link de navigación.
        public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
        {
            base.PrepareForSegue(segue, sender);


            if (segue.Identifier == "NavSegue")
            {

                if (TxtNombre.Text != "")
                {
                    //Obteniendo el nombre y el género.
                    string nombre = TxtNombre.Text;
                    string genero = StrGenero;


                    (segue.DestinationViewController as SecondViewController).Nombre = nombre;
                    (segue.DestinationViewController as SecondViewController).Genero = genero;


                    //Código para obtener la fecha.
                    string strDatePicker = DatePickerNacimiento.Date.ToString();
                    DateTime objDate = DateTime.Parse(strDatePicker);

                    //Extraer sólo la fecha, sin la hora y demás detalles.
                    CultureInfo mexicanLocale = new CultureInfo("es-MX");
                    string strDateFormatted = objDate.ToString("dd 'de' MMMM 'de' yyyy", mexicanLocale);

                    (segue.DestinationViewController as SecondViewController).FechaNacimiento = strDateFormatted;
                }
                else
                {
                    ShowMessage("Advertencia", "Llene todos los campos, por favor.");
                }

            }


        }

        #endregion

    }
}
