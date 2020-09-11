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
        //metodo para verificar que el usuario sea valido para login
        public UEncapUsuario verificarUsuario(UEncapUsuario verificar)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Correo.Equals(verificar.Correo) && x.Correo.Equals(verificar.Clave)).FirstOrDefault();
            }
        }
        //metodo de actualizacion de session

        public UEncapUsuario actualizarsession(UEncapUsuario actualizar)
        {
            using (var db = new Mapeo())
            {
                var resultado = db.usuario.FirstOrDefault(x => x.Correo == actualizar.Correo);
                if (resultado != null)
                {
                    resultado.Ip_ = null;
                    resultado.Mac_ = null;
                    resultado.Sesion = null;

                    db.SaveChanges();
                    return resultado;
                }
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

        //METODO PARA INSERTAR USUARIO AL MOMENTO DEL LOGIN
        public void InsertarUsuario(UEncapUsuario user)
        {
            using (var db = new Mapeo())
            {
                db.usuario.Add(user);
                db.SaveChanges();
            }
        }
    }

}
