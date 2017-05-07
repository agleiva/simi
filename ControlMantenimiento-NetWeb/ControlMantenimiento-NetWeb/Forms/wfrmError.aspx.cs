using System;
using ControlMantenimiento_NetWeb.BLL;

namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Funciones.MensajeError != null)
            {
                txtMensajeError.Text = Funciones.MensajeError;
            }            
        }
    }
}
