using System;

using UIKit;

namespace PracticaSegue
{
    public partial class SecondViewController : UIViewController
    {

        #region Class properties

        public string Nombre { get; set; }

        public string FechaNacimiento { get; set; }

        public string Genero { get; set; }

        #endregion



        #region Constructor

        protected SecondViewController(IntPtr handle) : base(handle)
        {
            
        }

        #endregion


        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //Se pintan los resultados obtenidos del FirstViewController en los Labels.
            LblNombre.Text = Nombre;
            LblNacimiento.Text = FechaNacimiento;
            LblGenero.Text = Genero;

        }

        #endregion
    }
}

