
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
    }
}

