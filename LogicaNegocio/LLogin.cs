
using Utilitarios;
using Datos;

namespace LogicaNegocio
{
    public class LLogin
    {

        public int login (UEncapUsuario usuario)
        {
            int mensaje;
            usuario = new DaoUsuario().verificarUsuario(usuario);

            if (usuario != null)
                mensaje = 1;
            else

                mensaje = 2;

            return mensaje;
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
    }
}

