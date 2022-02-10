using System;

namespace Prueba.FullStack.F2X.Recaudos.Entidades
{
    public class ReporteMensual
    {
        public string FechaConsulta { get; set; }
        
        public string Estacion { get; set; }

        public int TotalCantidad { get; set; }

        public decimal TotalValor { get; set; }
    }
}
