using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.FullStack.F2X.Recaudos.Negocio
{
    public class GestionApi<T>
    {

        #region Singleton
        private static GestionApi<T> instancia = null;
        private static readonly object padlock = new object();

        public static GestionApi<T> Instancia
        {
            get
            {
                lock (padlock)
                {
                    if (instancia == null)
                    {
                        instancia = new GestionApi<T>();
                    }
                    return instancia;
                }
            }
        }
        #endregion Singleton


        public string EjecutarPOST(T entidad, string url)
        {
            var peticion = (HttpWebRequest)WebRequest.Create(url);
            string json = JsonConvert.SerializeObject(entidad);

            peticion.Method = "POST";
            peticion.ContentType = "application/json";
            peticion.Accept = "application/json";

            using (var flujoEscritura = new StreamWriter(peticion.GetRequestStream()))
            {
                flujoEscritura.Write(json);
                flujoEscritura.Flush();
                flujoEscritura.Close();
            }

            try
            {
                using (WebResponse respuesta = peticion.GetResponse())
                {
                    using (Stream strReader = respuesta.GetResponseStream())
                    {
                        if (strReader == null)
                        {
                            return null;
                        }

                        using (StreamReader lector = new StreamReader(strReader))
                        {
                            return lector.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EjecutarGET(string fechaConsulta, string token, string url)
        {
            var peticion = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", url, fechaConsulta));

            peticion.Method = "GET";
            peticion.ContentType = "application/json";
            peticion.Accept = "application/json";
            peticion.Headers.Add("Authorization", string.Format("Bearer {0}", token));

            try
            {
                using (WebResponse respuesta = peticion.GetResponse())
                {
                    using (Stream strReader = respuesta.GetResponseStream())
                    {
                        if (strReader == null)
                        {
                            return null;
                        }

                        using (StreamReader lector = new StreamReader(strReader))
                        {
                            return lector.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
