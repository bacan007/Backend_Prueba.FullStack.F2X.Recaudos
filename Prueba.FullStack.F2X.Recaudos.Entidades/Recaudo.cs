
using System;

namespace Prueba.FullStack.F2X.Recaudos.Entidades
{
    public class Recaudo
    {

        public string Estacion { get; set; }

        public string Sentido { get; set; }

        public int Hora { get; set; }

        public string Categoria { get; set; }

        public int Cantidad { get; set; }

        public decimal ValorTabulado { get; set; }

        public DateTime FechaConsulta { get; set; }

        public string SFechaConsulta { get; set; }

    }
}
