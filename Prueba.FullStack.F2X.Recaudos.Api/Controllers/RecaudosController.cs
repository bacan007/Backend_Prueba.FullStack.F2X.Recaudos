using Prueba.FullStack.F2X.Recaudos.Api.Models;
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
    public class RecaudosController : ApiController
    {
        [Route("api/Recaudos/Guardar")]
        [HttpPost]
        public Respuesta Guardar([FromBody] Filtro filtro)
        {
            HeaderApiRequest headerApiRequest = new HeaderApiRequest(Request);

            Respuesta responseHeader = headerApiRequest.ValidarHeader();

            if (!responseHeader.Condicion)
            {
                return new RespuestaColeccion<List<Recaudo>>(responseHeader.Mensaje, responseHeader.Condicion);
            }

            GestionRecaudos.Instancia.GestionarRecaudos(filtro.FechaConsulta);

            Respuesta respuesta = new Respuesta(Constantes.OK, true);

            return respuesta;
        }

        [Route("api/Recaudos/Recaudos")]
        [HttpGet]
        public RespuestaColeccion<List<Recaudo>> Recaudos()
        {
            HeaderApiRequest headerApiRequest = new HeaderApiRequest(Request);

            Respuesta responseHeader = headerApiRequest.ValidarHeader();

            if (!responseHeader.Condicion)
            {
                return new RespuestaColeccion<List<Recaudo>>(responseHeader.Mensaje, responseHeader.Condicion);
            }

            var recaudos = GestionRecaudos.Instancia.Consultar();

            string mensaje = Constantes.SIN_REGISTROS;

            if (recaudos.Count > 0)
            {
                mensaje = Constantes.OK;
            }

            RespuestaColeccion<List<Recaudo>> respuesta = new RespuestaColeccion<List<Recaudo>>(mensaje, (mensaje == Constantes.OK), recaudos);

            return respuesta;
        }

        [Route("api/Recaudos/ReporteMensual")]
        [HttpGet]
        public RespuestaColeccion<List<ReporteMensual>> ReporteMensual()
        {
            HeaderApiRequest headerApiRequest = new HeaderApiRequest(Request);

            Respuesta responseHeader = headerApiRequest.ValidarHeader();

            if (!responseHeader.Condicion)
            {
                return new RespuestaColeccion<List<ReporteMensual>>(responseHeader.Mensaje, responseHeader.Condicion);
            }

            var reporte = GestionRecaudos.Instancia.ConsultarReporteMensual();

            string mensaje = Constantes.SIN_REGISTROS;

            if (reporte.Count > 0)
            {
                mensaje = Constantes.OK;
            }

            RespuestaColeccion<List<ReporteMensual>> respuesta = new RespuestaColeccion<List<ReporteMensual>>(mensaje, (mensaje == Constantes.OK), reporte);

            return respuesta;
        }

    }
}
