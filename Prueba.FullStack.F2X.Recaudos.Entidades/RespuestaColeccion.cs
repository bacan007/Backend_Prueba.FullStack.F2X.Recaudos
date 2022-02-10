
namespace Prueba.FullStack.F2X.Recaudos.Entidades
{
    public class RespuestaColeccion<T> : Respuesta
    {
        public T Resultado { get; set; }

        public RespuestaColeccion(string mensaje, bool condicion)
            : base(mensaje, condicion)
        {

        }

        public RespuestaColeccion(string message, bool condition, T resultado)
            : base(message, condition)
        {
            Resultado = resultado;
        }

    }
}
