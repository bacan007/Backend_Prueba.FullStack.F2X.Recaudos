using Prueba.FullStack.F2X.Recaudos.Datos.ORM;
using Prueba.FullStack.F2X.Recaudos.Entidades;
using Prueba.FullStack.F2X.Recaudos.Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.FullStack.F2X.Recaudos.Datos.Persistencia
{
    public class PersistenciaRecaudos<T> : IOperaciones<T>
    {
        #region Singleton
        private static PersistenciaRecaudos<T> instancia = null;
        private static readonly object padlock = new object();

        public static PersistenciaRecaudos<T> Instancia
        {
            get
            {
                lock (padlock)
                {
                    if (instancia == null)
                    {
                        instancia = new PersistenciaRecaudos<T>();
                    }
                    return instancia;
                }
            }
        }
        #endregion Singleton

        public void Insertar(T entidad)
        {
            Recaudo recaudo = entidad as Recaudo;

            using (RecaudosEntities contextoDB = new RecaudosEntities())
            {
                contextoDB.SP_InsertRecaudos(recaudo.Estacion,
                                            recaudo.Sentido,
                                            recaudo.Hora,
                                            recaudo.Categoria,
                                            recaudo.Cantidad,
                                            recaudo.ValorTabulado,
                                            recaudo.FechaConsulta);
            }
        }

        public void Truncar()
        {
            using (RecaudosEntities contextoDB = new RecaudosEntities())
            {
                contextoDB.SP_TruncateRecaudos();
            }
        }

        public List<Recaudo> Consultar()
        {
            List<Recaudo> recaudos = new List<Recaudo>();

            using (RecaudosEntities contextoDB = new RecaudosEntities())
            {
                var resultado = contextoDB.SP_GetRecaudos().ToList();

                recaudos = resultado.ConvertAll(x => new Recaudo
                {
                    Estacion = x.Estacion,
                    Categoria = x.Categoria,
                    Sentido = x.Sentido,
                    Hora = x.Hora,
                    Cantidad = x.Cantidad,
                    ValorTabulado = x.ValorTabulado,
                    FechaConsulta = x.FechaConsulta,
                    SFechaConsulta = x.FechaConsulta.ToShortDateString()
                });
            }

            return recaudos;
        }

        public List<ReporteMensual> ConsultarReporteMensual()
        {
            List<ReporteMensual> recaudos = new List<ReporteMensual>();

            using (RecaudosEntities contextoDB = new RecaudosEntities())
            {
                var resultado = contextoDB.SP_ReporteValorTabulado().ToList();

                recaudos = resultado.ConvertAll(x => new ReporteMensual
                {
                    Estacion = x.Estacion,
                    FechaConsulta = x.FechaConsulta.ToShortDateString(),
                    TotalCantidad = x.TotalCantidad.Value,
                    TotalValor = x.TotalValor.Value
                });
            }

            return recaudos;
        }

    }
}
