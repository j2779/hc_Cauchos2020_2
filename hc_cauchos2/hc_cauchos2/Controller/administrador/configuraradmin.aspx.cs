using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Utilitarios;

public partial class View_administrador_configuraradmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UEncapUsuario usuario = new UEncapUsuario();
        usuario = new LLogin().usuarioActivo2((string)Session["correo"]);

        if (usuario == null || Session["Valido"] == null)
        {
            Response.Redirect("../home.aspx");
        }
        if (usuario.Rol_id != 1)
        {
            Response.Redirect("../home.aspx");
        }

        //inicio componentes de edit componentes como invisibles
        TB_editCorreo.Visible = false;
        BTN_editarCorreo.Visible = false;
        TB_editarPass.Visible = false;
        BTN_editarPass.Visible = false;
        BTN_cancelar.Visible = false;
        BTN_cancelar2.Visible = false;


        UEncapUsuario usu = new UEncapUsuario();
        usu = new LLogin().login(usu);
    }

    protected void BTN_editarCorreo_Click(object sender, EventArgs e)
    {

    }

    protected void BTN_cancelar_Click(object sender, EventArgs e)
    {

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void BTN_editarPass_Click(object sender, EventArgs e)
    {

    }

    protected void BTN_cancelar2_Click(object sender, EventArgs e)
    {

    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }
}