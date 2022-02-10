using Prueba.FullStack.F2X.Recaudos.Entidades;
using Prueba.FullStack.F2X.Recaudos.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Prueba.FullStack.F2X.Recaudos.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SeguridadController : ApiController
    {

        
        [Route("api/Seguridad/GenerarToken")]
        [HttpPost]
        public TokenSeguridad GenerarToken([FromBody] Credenciales credenciales)
        {
            if (!string.IsNullOrEmpty(credenciales.UserName) && !string.IsNullOrEmpty(credenciales.Password))
            {
                return GestionSeguridad.Instancia.AutenticarToken(credenciales);
            }
            else
            {
                return new TokenSeguridad { Token = Constantes.MESSAGE_03 };
            }
        }

    }
}
