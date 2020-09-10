using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using LogicaNegocio;


public partial class View_Registro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PanelMensaje.Visible = false;
        PanelMensaje1.Visible = false;
        PanelMensaje2.Visible = false;
    }

    protected void BTN_registrar_Click(object sender, EventArgs e)
    {
        ClientScriptManager cm = this.ClientScript;
        //creo objeto para verificar correo 
        UEncapUsuario verificarCorreo = new UEncapUsuario();
        verificarCorreo.Correo = TB_correo.Text;
        //creo objeto para verificar identificacion
        UEncapUsuario verificarIdentificacion = new UEncapUsuario();
        verificarIdentificacion.Identificacion = TB_identificacion.Text;
        //envio secuencias de verificacion
        bool veriCorreo = new LLogin().verificarCorreo(verificarCorreo);
        bool veriIdentificacion = new LLogin().verificarIdentificacion(verificarIdentificacion);

        if (verificarCorreo == null && verificarIdentificacion == null)
        {

        }

        if (veriCorreo != true)
        {
            //cm.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert ( 'El correo ya se encuentra registrado' );</script>");
            //return;
            MostrarMensaje($"El correo ya se encuentra registrado");
            return;
        }
        if (veriIdentificacion != true)
        {
            //cm.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert ( 'La identificacion ya se encuentra registrada' );</script>");
            //return;
            MostrarMensaje($"La identificacion ya se encuentra registrada");
            return;
        }
    }

    private void MostrarMensaje(string mensaje)
    {
        LblMensaje.Text = mensaje;
        PanelMensaje.Visible = true;
    }

    private void MostrarMensaje1(string mensaje)
    {
        LblMensaje1.Text = mensaje;
        PanelMensaje1.Visible = true;
    }

    private void MostrarMensaje2(string mensaje)
    {
        LblMensaje2.Text = mensaje;
        PanelMensaje2.Visible = true;
    }

    protected void B_cerrar_mensaje1_Click(object sender, EventArgs e)
    {
        PanelMensaje.Visible = false;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PanelMensaje1.Visible = false;
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        PanelMensaje2.Visible = false;
    }
}