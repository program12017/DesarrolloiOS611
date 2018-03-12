using System;

using UIKit;

namespace PracticaSegue.Models
{
    public class GeneroPickerViewModel : UIPickerViewModel
    {
        static string[] generos = new string[]
        {
            "Masculino",
            "Femenino"
        };


        //Obtener el objeto seleccionado.
        public string SelectedItem { get; private set; }

        //El índice actual.
        int selectedIndex = 0;

        //Para conseguir el valor en el método Selected.
        FirstViewController controller;

        public GeneroPickerViewModel(FirstViewController controller)
        {
            this.controller = controller;

            //Poner el valor inicial.
            controller.StrGenero = generos[0];
        }


        public override nint GetComponentCount(UIPickerView v)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView v, nint component)
        {
            return generos.Length;
        }

        public override string GetTitle(UIPickerView v, nint row, nint component)
        {
            return generos[(int)row];
        }

        public override void Selected(UIPickerView v, nint row, nint component)
        {
            selectedIndex = (int)row;

            //Actualizar el valor constantemente.
            controller.StrGenero = generos[(int)row];

        }

        public override nfloat GetComponentWidth(UIPickerView v, nint component)
        {
            if (component == 0)
            {
                return 240f;
            }
            else
            {
                return 40f;
            }

        }

        public override nfloat GetRowHeight(UIPickerView v, nint component)
        {
            return 40f;
        }
    }
}
