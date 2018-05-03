using System;

using UIKit;
using System.Collections.Generic;
using Foundation;

namespace PracticaTablaMultiplicar
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        #region Class Variables

        List<String> lstMultiplication;
        int multiplicationNum;
        int maxLimit;

        UITextField limitBox;

        #endregion

        #region Constructors

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #endregion

        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //Se coloca un registro de demostración.
            lstMultiplication = new List<string>();
            lstMultiplication.Add("0 x 0 = 0");


            //Asignar el DataSource y el Delegate.
            MultiplicationTableView.DataSource = this;
            MultiplicationTableView.Delegate = this;
        }

        #endregion


        #region User Interactions

        partial void BtnShow_Clicked(Foundation.NSObject sender)
        {
            //ActionSheet para seleccionar tabla de multiplicar.
            UIAlertController alert = UIAlertController.Create("Escoja uno de los siguientes números:", null, UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("1", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("2", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("3", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("4", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("5", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("6", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("7", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("8", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("9", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("10", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("11", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("12", UIAlertActionStyle.Default, SetMultiplicationNumber));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));


            PresentViewController(alert, animated: true, completionHandler: null);
        }

        //Esta es la acción que se ejecuta al seleccionar uno de los números.
        void SetMultiplicationNumber(UIAlertAction obj)
        {
            //Se parsea el número seleccionado en la primera alerta.
            multiplicationNum = int.Parse(obj.Title);

            //Se muestra la segunda alerta.
            UIAlertController alert = UIAlertController.Create("Ahora inserte el número máximo entero de la tabla de multiplicar (Del -999 al 999):", null, UIAlertControllerStyle.Alert);
            alert.AddTextField(IngresarLimiteMultiplicacion);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, EstablecerLimiteMultiplicacion));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));


            PresentViewController(viewControllerToPresent: alert, animated: true, completionHandler: null);
        }

        //El ConfigurationHandler del TextField de la alerta.
        void IngresarLimiteMultiplicacion(UITextField obj)
        {
            //Se coloca el tipo de teclado al Textfield de la alerta.
            obj.KeyboardType = UIKeyboardType.NumbersAndPunctuation;

            //El objeto se iguala a una
            limitBox = obj;
        }

        //La acción ejecutada al dar OK en la segunda alerta, cuando se escoge el la extensión de la tabla de multiplicar.
        void EstablecerLimiteMultiplicacion(UIAlertAction obj)
        {
            //Validar que el valor de la caja de texto sea númerico entero.
            if (int.TryParse(limitBox.Text, out maxLimit))
            {
                //Verificar que usuario no haya colocado cero con signo.
                if (limitBox.Text != "-0" && limitBox.Text != "+0")
                {
                    //Se verifica que se esté en el rango establecido.
                    if (maxLimit >= -999 && maxLimit <= 999)
                    {
                        //Se inicializa la lista.
                        lstMultiplication = new List<string>();

                        //Se llama la función para llenar la lista y pintarla.
                        RefillMultiplicationList();
                    }
                    else
                    {
                        UIAlertController alert = UIAlertController.Create("ERROR:", "Ese valor se sale del rango establecido.", UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        PresentViewController(viewControllerToPresent: alert, animated: true, completionHandler: null);
                    }
                }
                else
                {
                    UIAlertController alert = UIAlertController.Create("ERROR:", "El cero no debe llevar signo.", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    PresentViewController(viewControllerToPresent: alert, animated: true, completionHandler: null);
                }
            }
            else
            {
                UIAlertController alert = UIAlertController.Create("ERROR:", "Sólo puede capturar números enteros.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                PresentViewController(viewControllerToPresent: alert, animated: true, completionHandler: null);
            }



        }



        #endregion


        #region Multiplication Process

        void RefillMultiplicationList()
        {
            //Si los números son positivos.
            if (maxLimit >= 0)
            {
                for (int i = 0; i <= maxLimit; i++)
                {
                    int val = multiplicationNum * i;

                    lstMultiplication.Add($"{multiplicationNum} x {i} = {val}");
                }
            }
            //Si los números son negativos.
            else
            {
                for (int i = 0; i >= maxLimit; i--)
                {
                    int val = multiplicationNum * i;

                    lstMultiplication.Add($"{multiplicationNum} x {i} = {val}");
                }
            }


            InvokeOnMainThread(() => {

                MultiplicationTableView.ReloadData();
            });

        }

        #endregion


        #region UITableView DataSource

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return lstMultiplication.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = MultiplicationTableView.DequeueReusableCell("BasicTableCell", indexPath);

            cell.TextLabel.Text = $"{lstMultiplication[indexPath.Row]}";

            return cell;
        }

        #endregion

    }
}
