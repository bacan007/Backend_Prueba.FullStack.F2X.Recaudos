
namespace Prueba.FullStack.F2X.Recaudos.Entidades
{
    public class Respuesta
    {
        public long? Id { get; set; }

        public string Mensaje { get; set; }

        public bool Condicion { get; set; }

        public Respuesta(string mensaje, bool condicion)
        {
            Id = null;
            Mensaje = mensaje;
            Condicion = condicion;
        }

        public Respuesta(long id, string mensaje, bool condicion)
        {
            Id = id;
            Mensaje = mensaje;
            Condicion = condicion;
        }
    }
}
