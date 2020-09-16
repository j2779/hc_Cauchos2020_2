using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json;
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
            bool mensaje;
            usuario = new DaoUsuario().verificarCorreo(usuario);
            if( usuario != null)
            {
                mensaje = false;
            }
            else
            {
                mensaje = true;
            }

            return mensaje;
        }
        public UEncapUsuario busquedaToken(string token)
        {
            UEncapUsuario usuario = new UEncapUsuario();
            usuario = new DaoUsuario().BuscarToken(token);


            return usuario;
        }
        public UEncapUsuario verificarCorreoRecuperacion(UEncapUsuario usuario)
        {

            usuario = new DaoUsuario().verificarCorreo(usuario);
            if (usuario != null)
            {
                return usuario;
            }
            else
            {
                return null;
            }


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
        public string encriptar(string input)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }
    }
}

