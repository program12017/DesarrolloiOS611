using System;

using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace PullToRefresh.Models
{
    //Clase Singleton.
    public class CitiesManager
    {
        #region Singleton

        //Con método anónimo que retorna una instancia del modelo.
        static readonly Lazy<CitiesManager> lazy = new Lazy<CitiesManager>(() => new CitiesManager());

        //Cuando se accede al valor lazy.Value, se elabora una instancia y se guarda en memoria.
        //Así compartiendo la información a través de las clases.
        public static CitiesManager SharedInstance { get => lazy.Value; }

        #endregion


        #region Class Variables

        //Una sóla instancia por toda la aplicación.
        HttpClient httpClient;

        //Diccionario que contendrá los datos del archivo JSON.
        Dictionary<string, List<string>> cities;

        #endregion


        #region Events

        //Se coloca, como tipo de EventHandler, a la clase auxiliar
        //hecha en la parte inferior.
        public event EventHandler<CitiesEventArgs> CitiesFetched;

        public event EventHandler<ErrorEventArgs> FetchCitiesFailed;

        #endregion


        #region Constructors

        //Se hace el constructor de manera privada.
        //De esta forma sólo se puede instanciar con la propiedad estática.
        CitiesManager()
        {
            httpClient = new HttpClient();
        }

        #endregion

        #region Public Functionality

        public Dictionary<string, List<string>> GetDefaultCities()
        {
            var citiesJson = File.ReadAllText("cities-incomplete.json");

            //Vamos a parsear el Json, usando Json.net (Newtonsoft)
            //Pasarlo al tipo de Dictionary declarado en Class Variables.
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
        }

        //Preferentemente, no hacer las descargas en los hilos principales.
        public void FetchCities()
        {
            //Manda a abrir un nuevo hilo secundario para el siguiente método asíncrono.
            Task.Factory.StartNew(FetchCitiesAsync);

            //Un método dentro de un método, 
            async Task FetchCitiesAsync()
            {
                try
                {
                    //Como se está en un hilo secundario, se tiene que avisar
                    //al Controller que ya están los datos solicitados.
                    //1.- A través de eventos. (Events/Delegate) (Es el que más conviene en C#) *********
                    //2.- A través de notificaciones. (NSNotificationCenter)
                    //3.- (Sólo aplica cuando se está dentro de un ViewController) A través de Unwind Segue.


                    //Si esto verdadero, significa que nada ha ocurrido.
                    if (CitiesFetched == null)
                    {
                        return;
                    }
                    else
                    {
                        var citiesJson = await httpClient.GetStringAsync("https://dl.dropbox.com/s/0adq8yw6vd5r6bj/cities.json?dl=0");

                        cities = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);

                        var e = new CitiesEventArgs(cities);
                        CitiesFetched(this, e);
                    }

                }
                catch (Exception ex)
                {
                    //Como se está en un hilo secundario, se tiene que avisar
                    //al Controller que algo falló.
                    //1.- A través de eventos. (Events/Delegate)
                    //2.- A través de notificaciones. (NSNotificationCenter)
                    //3.- (Sólo aplica cuando se está dentro de un ViewController) A través de Unwind Segue.

                    if (FetchCitiesFailed == null)
                    {
                        return;
                    }
                    else
                    {
                        var e = new ErrorEventArgs(ex.Message);

                        FetchCitiesFailed(this, e);
                    }

                }
            }
        }

        #endregion
    }


    //Clases auxiliares.
    public class CitiesEventArgs : EventArgs
    {
        //Propiedad cuya asignación de valores es privado.
        public Dictionary<string, List<string>> Cities { get; private set; }

        public CitiesEventArgs(Dictionary<string, List<string>> cities)
        {
            Cities = cities;
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }
}
