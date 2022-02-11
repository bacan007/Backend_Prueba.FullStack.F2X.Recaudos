using Newtonsoft.Json;
using Prueba.FullStack.F2X.Recaudos.Datos.Persistencia;
using Prueba.FullStack.F2X.Recaudos.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;

namespace Prueba.FullStack.F2X.Recaudos.Negocio
{
    public class GestionRecaudos
    {

        #region Singleton
        private static GestionRecaudos instancia = null;
        private static readonly object padlock = new object();

        public static GestionRecaudos Instancia
        {
            get
            {
                lock (padlock)
                {
                    if (instancia == null)
                    {
                        instancia = new GestionRecaudos();
                    }
                    return instancia;
                }
            }
        }
        #endregion Singleton

        public void GestionarRecaudos(string fechaConsulta)
        {
            var tokenSeguridad = GestionSeguridad.Instancia.GenerarToken();

            var respuesta = GestionApi<Credenciales>.Instancia.EjecutarGET(fechaConsulta, tokenSeguridad.Token, GestionSeguridad.Instancia.ObtenerURL(Constantes.CONTEO_VEHICULOS));

            var recaudosConteo = JsonConvert.DeserializeObject<List<Recaudo>>(respuesta);

            respuesta = GestionApi<Credenciales>.Instancia.EjecutarGET(fechaConsulta, tokenSeguridad.Token, GestionSeguridad.Instancia.ObtenerURL(Constantes.RECAUDO_VEHICULOS));

            var recaudosValorTabulado = JsonConvert.DeserializeObject<List<Recaudo>>(respuesta);

            var recaudos = Combinar(recaudosConteo, recaudosValorTabulado, fechaConsulta);

            Procesar(recaudos);
        }

        private void Procesar(List<Recaudo> recaudos)
        {
            using (TransactionScope transaccion = new TransactionScope())
            {
                recaudos.ForEach(recaudo =>
                {
                    InsertarRecaudo(recaudo);
                });

                transaccion.Complete();
            }
        }

        private List<Recaudo> Combinar(List<Recaudo> recaudosConteo, List<Recaudo> recaudosValorTabulado, string fechaConsulta)
        {
            var recaudos = recaudosConteo.Except(recaudosValorTabulado, new Comparar()).ToList();

            recaudos.ForEach(recaudo =>
            {
                var auxRecaudo = recaudosValorTabulado.Find(x => ObtenerLlave(x) == ObtenerLlave(recaudo));

                if (auxRecaudo != null)
                {
                    recaudo.ValorTabulado = auxRecaudo.ValorTabulado;
                    recaudo.FechaConsulta = Convert.ToDateTime(fechaConsulta);
                }
            });

            return recaudos;
        }

        private string ObtenerLlave(Recaudo x)
        {
            return string.Format("{0}{1}{2}{3}", x.Estacion, x.Sentido, x.Categoria, x.Hora);
        }

        private void InsertarRecaudo(Recaudo recaudo)
        {
            PersistenciaRecaudos<Recaudo>.Instancia.Insertar(recaudo);
        }

        public List<Recaudo> Consultar()
        {
            return PersistenciaRecaudos<dynamic>.Instancia.Consultar();
        }

        public List<ReporteMensual> ConsultarReporteMensual()
        {
            return PersistenciaRecaudos<dynamic>.Instancia.ConsultarReporteMensual();
        }

    }
}
