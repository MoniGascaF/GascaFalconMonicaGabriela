using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PullToRefresh.Models
{
    public class CitiesManager
    {
        #region Singleton
        static readonly Lazy<CitiesManager> lazy = new Lazy<CitiesManager>(() => new CitiesManager());
        public static CitiesManager SharedInstance { get => lazy.Value; }
        #endregion

        #region Variables de Clase
        HttpClient httpClient;
        Dictionary<string, List<string>> cities;
        #endregion

        #region Eventos
        public event EventHandler<CitiesEventArgs> CitiesFetched;
        public event EventHandler<EventArgs> FetchCitiesFaild;
        #endregion

        #region Constructor
        private CitiesManager()
        {
            httpClient = new HttpClient();
        }
        #endregion

        #region Funcionalidad
        public Dictionary<string, List<string>> GetDefaultCities()
        {
            var citiesJson = File.ReadAllText("cities-incomplete.json");
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
        }
        //obtener info por internet
        public void FetchCities()
        {
            Task.Factory.StartNew(FetchCitiesAsync);
            async Task FetchCitiesAsync()
            {
                try
                {
                    var citiesJson = await httpClient.GetStringAsync("https://dl.dropbox.com/s/0adq8yw6vd5r6bj/cities.json?=dl=0");
                    cities = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
                    //es avisarle al controller que ya estan disponibles los datos
                    //1.- a trave de eventos
                    //2.- A traves de notificaciones
                    //3.- Solo cuando estas dentro de un viewController (A traves de unwind Segues)
                    if (CitiesFetched == null)
                        return;
                    var e = new CitiesEventArgs(cities);
                    CitiesFetched(this, e);
                }

                catch (Exception ex)
                {
                    if (CitiesFetched == null)
                        return;
                    FetchCitiesFaild(this, new EventArgs());
                }
            }
        }
        #endregion
        public class CitiesEventArgs : EventArgs
        {
            public Dictionary<string, List<string>> Cities { get; private set; }
            public CitiesEventArgs(Dictionary<string, List<string>> cities)
            {
                Cities = cities;
            }
        }
    }
}
