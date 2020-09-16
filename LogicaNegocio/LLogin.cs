
using Utilitarios;
using Datos;

namespace LogicaNegocio
{
    public class LLogin
    {
        // logica de negocio login
        public UEncapUsuario login (UEncapUsuario usuario)
        {
            UEncapUsuario mensaje = new UEncapUsuario();
            mensaje = new DaoUsuario().verificarUsuario(usuario);

            return mensaje;
        }    
        // actualizacion de usuario
        public string Actualizar (UEncapUsuario userb)
        {
            string aviso;
            userb = new DaoUsuario().actualizarsession(userb);

            if (userb != null)
            {
                aviso = ("$Se han cerrado las sessiones antiguas");
            }
            else {

                aviso = ("$NO Se han cerrado las sessiones antiguas");
            }
            return aviso;
        }
        public bool verificarCorreo(UEncapUsuario usuario)
        {

            bool respuesta;
            usuario = new DaoUsuario().verificarCorreo(usuario);
            if( usuario != null)
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }

            return respuesta;
        }
        public bool verificarIdentificacion(UEncapUsuario usuario)
        {
            bool respuesta;
            usuario = new DaoUsuario().verificarIdentificacion(usuario);
            if (usuario != null)
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }


        public void insertarUsuario(UEncapUsuario usuario)
        {
             new DaoUsuario().InsertarUsuario(usuario);
        }

        public void actualizarUsuario(UEncapUsuario usuario)
        {
            new DaoUsuario().ActualizarUsuario(usuario);
        }

       public UEncapUsuario usuarioActivo2(string correo)
        {
           var usuario = new DaoUsuario().UsuarioActivo2(correo);
           return usuario;
        }

        public void insertarEmpleado(UEncapUsuario usuario)
        {
            new DAOAdmin().InsertarEmpleado(usuario);
        }
    }
}

