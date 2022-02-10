using Prueba.FullStack.F2X.Recaudos.Entidades;
using Prueba.FullStack.F2X.Recaudos.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Prueba.FullStack.F2X.Recaudos.Api.Models
{
    public class HeaderApiRequest
    {
        private readonly HttpRequestMessage Request;

        public HeaderApiRequest(HttpRequestMessage Request)
        {
            this.Request = Request;
        }

        public Respuesta ValidarHeader()
        {
            var headers = Request.Headers;

            if (!headers.Contains("Token"))
            {
                return new Respuesta(Constantes.MESSAGE_01, false);
            }

            string token = headers.GetValues("Token").First();

            bool isValid = GestionSeguridad.Instancia.ValidarToken(token);

            if (isValid)
            {
                return new Respuesta(Constantes.OK, true);
            }

            return new Respuesta(Constantes.MESSAGE_02, false);
        }
    }
}