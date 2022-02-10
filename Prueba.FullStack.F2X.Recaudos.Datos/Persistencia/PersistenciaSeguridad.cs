using Prueba.FullStack.F2X.Recaudos.Datos.ORM;
using Prueba.FullStack.F2X.Recaudos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.FullStack.F2X.Recaudos.Datos.Persistencia
{
    public class PersistenciaSeguridad
    {
        #region Singleton
        private static PersistenciaSeguridad instancia = null;
        private static readonly object padlock = new object();

        public static PersistenciaSeguridad Instancia
        {
            get
            {
                lock (padlock)
                {
                    if (instancia == null)
                    {
                        instancia = new PersistenciaSeguridad();
                    }
                    return instancia;
                }
            }
        }
        #endregion Singleton

        public string AutenticarToken()
        {
            string token = null;

            using (RecaudosEntities contextoDB = new RecaudosEntities())
            {
                token = contextoDB.SP_InsertarToken().FirstOrDefault();
            }

            return token;
        }

        public bool ValidarToken(string token)
        {
            using (RecaudosEntities contextoDB = new RecaudosEntities())
            {
                var result = contextoDB.SP_ValidarToken(token).FirstOrDefault();

                if (result.Estado == Constantes.ESTADO)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
