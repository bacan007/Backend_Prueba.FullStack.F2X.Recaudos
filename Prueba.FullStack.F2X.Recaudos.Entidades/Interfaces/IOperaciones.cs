using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.FullStack.F2X.Recaudos.Entidades.Interfaces
{
    public interface IOperaciones<T>
    {

        void Insertar(T entity);

        void Truncar();

    }
}
