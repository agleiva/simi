using System;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento.Model;
using ControlMantenimiento_NetWeb.BLL;


namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmCambioClave : System.Web.UI.Page
    {
        private Operario operario;

        protected void Page_Init(object sender, EventArgs e)
        {
            IControlador icontrolador = Funciones.CrearControlador();
           operario = (Operario)icontrolador.ObtenerRegistro(BLL.Funciones.UsuarioConectado.ToString(),"O");
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TIPO_USUARIO"] == null)
            {
                Response.Redirect("~/Forms/wfrmAcceso.aspx");
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Verificar())
            {
                Guardar();
            }
        }

        private void Guardar()
        {
            IControlador icontrolador = Funciones.CrearControlador();
            if (icontrolador.GuardarCambioClave(txtClaveNueva.Text))
            { 
                Response.Redirect("~/Forms/wfrmRespuesta.aspx");
            }
            else
            {
                Response.Redirect("~/Forms/wfrmError.aspx");
            }
        }
       
        private bool Verificar()
        {
            txtClave.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtClave.Text);
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtClave.Focus();
                return false;
            }
            if (txtClave.Text.Length < 6)
            {
                txtMensajeError.Text = Mensajes.Mensaje21;
                txtClave.Focus();
                return false;
            }
            if (txtClave.Text != operario.clave)
            {
                txtMensajeError.Text = Mensajes.Mensaje3;
                txtClave.Focus();
                return false;
            }            
            txtClaveNueva.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtClaveNueva.Text);
            if (string.IsNullOrEmpty(txtClaveNueva.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtClaveNueva.Focus();
                return false;
            }
            if (txtClaveNueva.Text.Length < 6)
            {
                txtMensajeError.Text = Mensajes.Mensaje21;
                txtClaveNueva.Focus();
                return false;
            }
            if (txtClaveNueva.Text == txtClave.Text)
            {
                txtMensajeError.Text = Mensajes.Mensaje4;
                txtClaveNueva.Focus();
                return false;
            }
            txtConfirmar.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtConfirmar.Text);
            if (string.IsNullOrEmpty(txtConfirmar.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtConfirmar.Focus();
                return false;
            }
            if (txtConfirmar.Text.Length < 6)
            {
                txtMensajeError.Text = Mensajes.Mensaje21;
                txtConfirmar.Focus();
                return false;
            }
            if (txtConfirmar.Text != txtClaveNueva.Text)
            {
                txtMensajeError.Text = Mensajes.Mensaje5;
                txtConfirmar.Focus();
                return false;
            }

            return true;
        }

    }
}
