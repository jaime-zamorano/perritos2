using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace perritos2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Raza racita = new Raza();
            IList<Raza> listaRaza = new List<Raza>();
            SubRaza superro = new SubRaza();

            WeatherForecast envio = new WeatherForecast();
            IList<WeatherForecast> listenvio = new List<WeatherForecast>();

            string URL = "https://dog.ceo/api/breeds/list/all ";

            string json = new WebClient().DownloadString(URL);

  
            dynamic m = JsonConvert.DeserializeObject(json, typeof(object));



            foreach (var i in m)
            {

                    var jsonValue1 = i.Value;


                var y = i.Value.Path;
                if (y == null)
                {

                    
                    int j = 0;
                    foreach (var item in jsonValue1)
                    {

                        string s = item.Name;

                        j = j + 1;
                        racita.RazaNombre = s;
                        racita.ImagenURL = getimagen(racita.RazaNombre);
                        racita.idRaza = j.ToString();
                        string h = insert(racita);
                        //para mostrar
                        envio.idraza = racita.idRaza;
                        envio.imagen = racita.ImagenURL;
                        envio.raza = racita.RazaNombre;
                        racita = new Raza();



                        if (item.Value.Count > 0) {
                         var r = item.Value;
                            string muestra = "";
                            for (int p = 0; p < item.Value.Count; p++)
                            {
                                superro.idRaza = j.ToString();
                                superro.Nombre = r[p].Value;
                                string k = insertsub(superro);
                                // para mostrar
                               
                                muestra = muestra + "," + superro.Nombre;

                                envio.subraza = muestra;
                                superro = new SubRaza();


                            }
                        }

                        listenvio.Add(envio);

                        envio = new WeatherForecast();

                    }

                }





            }

            Array array = new int[listenvio.Count];

    
            return listenvio;


            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }



        public string getimagen(string raza)
        {

            string urlimagen = "";

            string URL = "https://dog.ceo/api/breed/"+ raza.Trim() + "/images";

            string json = new WebClient().DownloadString(URL);
           
            
            dynamic m = JsonConvert.DeserializeObject(json, typeof(object));


            foreach (var i in m)
            {
                if (i.Value.Path == "message") {
                    var jsonimagen = i.Value;
                    string y = (string)jsonimagen.First;

                    urlimagen = y;
                }


            }


                return urlimagen;

        }


        public string insert(Raza perro) {

            string rest = "";

            conexion conect = new conexion();

            List<DataRow> dr = new List<DataRow>();

            string[,] parametros = new string[,]
            {
                     {"@idRaza", perro.idRaza},
                     {"@Raza", perro.RazaNombre},
                     {"@ImagenURL", perro.@ImagenURL}

            };

            dr = conect.CallBaseSqlParmetros("insertraza", parametros);


            return rest;

        
        }

        public string insertsub(SubRaza perro)
        {

            string rest = "";

            conexion conect = new conexion();

            List<DataRow> dr = new List<DataRow>();

            string[,] parametros = new string[,]
            {
                     {"@idRaza", perro.idRaza},
                     {"@Nombre", perro.Nombre}
                     

            };

            dr = conect.CallBaseSqlParmetros("insertsubraza", parametros);


            return rest;


        }



    }
}



