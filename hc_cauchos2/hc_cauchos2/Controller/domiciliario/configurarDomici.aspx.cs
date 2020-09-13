using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_domiciliario_configurarDomici : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //inicio componentes de edit componentes como invisibles
        TB_editCorreo.Visible = false;
        BTN_editarCorreo.Visible = false;
        TB_editarPass.Visible = false;
        BTN_editarPass.Visible = false;
        BTN_cancelar.Visible = false;
        BTN_cancelar2.Visible = false;

    }
}