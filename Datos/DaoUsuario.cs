using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace Datos
{
    public class DaoUsuario
    {
        //metodo para verificar que el usuario sea valido
        public UEncapUsuario verificarUsuario(UEncapUsuario verificar)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Correo.Equals(verificar.Correo) && x.Correo.Equals(verificar.Clave)).FirstOrDefault();
            }
        }
    }

}
