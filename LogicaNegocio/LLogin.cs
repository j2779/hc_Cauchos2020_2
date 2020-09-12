
using Utilitarios;
using Datos;

namespace LogicaNegocio
{
    public class LLogin
    {
        // logica de negocio login
        public bool login (UEncapUsuario usuario)
        {
            bool mensaje;
            usuario = new DaoUsuario().verificarUsuario(usuario);

            if (usuario != null)
                mensaje = false;
            else

                mensaje = true;

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
    }
}

