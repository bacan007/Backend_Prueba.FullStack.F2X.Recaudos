using Newtonsoft.Json;
using Prueba.FullStack.F2X.Recaudos.Datos.Persistencia;
using Prueba.FullStack.F2X.Recaudos.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.FullStack.F2X.Recaudos.Negocio
{
    public class GestionSeguridad
    {

        #region Singleton
        private static GestionSeguridad instancia = null;
        private static readonly object padlock = new object();

        public static GestionSeguridad Instancia
        {
            get
            {
                lock (padlock)
                {
                    if (instancia == null)
                    {
                        instancia = new GestionSeguridad();
                    }
                    return instancia;
                }
            }
        }
        #endregion Singleton

        public TokenSeguridad GenerarToken()
        {
            var credenciales = ObtenerCredenciales();

            var respuesta = GestionApi<Credenciales>.Instancia.EjecutarPOST(credenciales, ObtenerURL(Constantes.LOGIN));

            return JsonConvert.DeserializeObject<TokenSeguridad>(respuesta);
        }

        public string ObtenerURL(string recurso)
        {
            return string.Format("{0}{1}", ConfigurationManager.AppSettings["Host"].ToString(), recurso);
        }

        private Credenciales ObtenerCredenciales()
        {
            return new Credenciales
            {
                UserName = ConfigurationManager.AppSettings["Usuario"].ToString(),
                Password = ConfigurationManager.AppSettings["Contrasena"].ToString()
            };
        }

        public TokenSeguridad AutenticarToken(Credenciales credenciales)
        {
            var credencialesActivas = ObtenerCredenciales();

            if (credenciales.UserName == credencialesActivas.UserName
                && credenciales.Password == credencialesActivas.Password)
            {
                string token = PersistenciaSeguridad.Instancia.AutenticarToken();

                return new TokenSeguridad { Token = token };
            }

            return new TokenSeguridad { Token = Constantes.MESSAGE_04 };
        }

        public bool ValidarToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            return PersistenciaSeguridad.Instancia.ValidarToken(token);
        }

    }
}
