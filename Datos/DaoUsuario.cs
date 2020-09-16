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
                return db.usuario.Where(x => x.Correo.Equals(verificar.Correo) && x.Clave.Equals(verificar.Clave)).FirstOrDefault();
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
                    
                }
                return resultado;
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


        //METODO PARA ACTUALIZAR USUARIOS 
        public void ActualizarUsuario(UEncapUsuario user)
        {
            using (var db = new Mapeo())
            {

                var resultado = db.usuario.SingleOrDefault(b => b.User_id == user.User_id);
                if (resultado != null)
                {
                    resultado.Nombre = user.Nombre;
                    resultado.Apellido = user.Apellido;
                    resultado.Correo = user.Correo;
                    resultado.Clave = user.Clave;
                    resultado.Fecha_nacimiento = user.Fecha_nacimiento;
                    resultado.Identificacion = user.Identificacion;
                    resultado.Token = user.Token;
                    resultado.Estado_id = user.Estado_id;
                    resultado.Rol_id = user.Rol_id;
                    resultado.Tiempo_token = user.Tiempo_token;
                    resultado.Sesion = user.Sesion;
                    resultado.Last_modify = DateTime.Now;
                    resultado.Ip_ = user.Ip_;
                    resultado.Mac_ = user.Mac_;

                    db.SaveChanges();
                }

            }

        }

        //METODO CONSULTAR USUARIO ACTIVO
        public UEncapUsuario UsuarioActivo2(string correo)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Correo == correo).FirstOrDefault();
            }
        }
        //METODO PARA VERIFICAR TOKEN DE RECUPERACION EN LOGIN 
        public UEncapUsuario BuscarToken(string token)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Token.Equals(token) && x.Estado_id == 2).FirstOrDefault();
            }

        }

    }

}
