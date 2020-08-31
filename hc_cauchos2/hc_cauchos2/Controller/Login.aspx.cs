using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using LogicaNegocio;

public partial class View_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BTN_ingresar_Click(object sender, EventArgs e)
    {
        UEncapUsuario usuario = new UEncapUsuario();
        usuario.Correo = TB_correo.Text;
        usuario.Correo = TB_contraseña.Text;

        int val = new LLogin().login(usuario);

        /*if (val == 1)
            Response.Redirect("usuario.aspx");
        if (val == 2)
            LB_mensaje.Text = "usuario incorrecto";*/


    }
}