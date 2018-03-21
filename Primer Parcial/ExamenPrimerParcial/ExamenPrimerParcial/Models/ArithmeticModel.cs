using System;
using System.Globalization;
using ExamenPrimerParcial.Enums;
namespace ExamenPrimerParcial.Models
{
    public class ArithmeticModel
    {
        #region Class variables

        double number1;
        double number2;
        ArithmeticEnum operation;

        #endregion


        #region Dataviews

        //String result;
        public String Result
        {
            get
            {
                return CalculateResult();
            }
        }

        #endregion


        #region Constructors

        public ArithmeticModel() : this(0, 0, ArithmeticEnum.Addition)
        {

        }

        public ArithmeticModel(double pNumber1, double pNumber2, ArithmeticEnum pOperation)
        {
            number1 = pNumber1;
            number2 = pNumber2;
            operation = pOperation;
        }

        #endregion


        #region Internal functionality

        String CalculateResult()
        {
            double resultNumber;

            switch (operation)
            {
                case ArithmeticEnum.Addition:
                    
                    resultNumber = AdditionOperation();
                    break;

                case ArithmeticEnum.Substraction:

                    resultNumber = SubstractionOperation();
                    break;

                case ArithmeticEnum.Multiplication:

                    resultNumber = MultiplicationOperation();
                    break;

                case ArithmeticEnum.Division:

                    resultNumber = DivisionOperation();
                    break;

                default:
                    resultNumber = 0;
                    break;
            }


            //Formatting the number.
            CultureInfo mexicanLocale = new CultureInfo("es-MX");

            return resultNumber.ToString("n", mexicanLocale);

        }

        double AdditionOperation()
        {
            return number1 + number2;
        }

        double SubstractionOperation()
        {
            return number1 - number2;
        }

        double MultiplicationOperation()
        {
            return number1 * number2;
        }

        double DivisionOperation()
        {
            return (number2 != 0) ? number1 / number2 : 0;
        }

        #endregion

    }
}
