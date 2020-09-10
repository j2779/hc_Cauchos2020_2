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

        //METODO PARA VERIFICAR SI EXISTE REGISTRADO EL CORREO
        public UEncapUsuario verificarCorreo(UEncapUsuario verificar)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Correo.Equals(verificar.Correo)).FirstOrDefault();
            }
        }
        //METODO PARA VERIFICAR SI EXISTE REGISTRADA LA IDENTIFICACION
        public UEncapUsuario verificarIdentificacion(UEncapUsuario verificar)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Correo.Equals(verificar.Correo) || x.Identificacion.Equals(verificar.Identificacion)).FirstOrDefault();
            }
        }
    }

}
