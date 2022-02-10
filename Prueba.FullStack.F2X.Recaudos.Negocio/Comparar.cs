using Prueba.FullStack.F2X.Recaudos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.FullStack.F2X.Recaudos.Negocio
{
    public class Comparar : IEqualityComparer<Recaudo>
    {
        public bool Equals(Recaudo x, Recaudo y)
        {
            return x.Estacion == y.Estacion 
                    && x.Sentido == y.Sentido 
                    && x.Categoria == y.Categoria 
                    && x.Hora == y.Hora 
                    && x.FechaConsulta == y.FechaConsulta;
        }

        public int GetHashCode(Recaudo obj)
        {
            return string.Format("{0}{1}{2}{3}{4}", 
                                    obj.Estacion, 
                                    obj.Sentido, 
                                    obj.Categoria, 
                                    obj.Hora, 
                                    obj.FechaConsulta.ToShortDateString())
                .GetHashCode() ^ obj.ValorTabulado.GetHashCode();
        }
    }
}
