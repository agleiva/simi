using System;
using ControlMantenimiento.Business;
using ControlMantenimiento.Model;
using Funciones = ControlMantenimiento_NetWeb.BLL.Funciones;


namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmCambioClave : System.Web.UI.Page
    {
        private Operario operario;

        protected void Page_Init(object sender, EventArgs e)
        {
            var controlador = Funciones.CrearControlador();
           operario = (Operario)controlador.ObtenerRegistro(BLL.Funciones.UsuarioConectado.ToString(),"O");
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
            var controlador = Funciones.CrearControlador();
            if (controlador.GuardarCambioClave(txtClaveNueva.Text))
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
            txtClave.Text = txtClave.Text.Trim();
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
            if (txtClave.Text != operario.Clave)
            {
                txtMensajeError.Text = Mensajes.Mensaje3;
                txtClave.Focus();
                return false;
            }            
            txtClaveNueva.Text = txtClaveNueva.Text.Trim();
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
            txtConfirmar.Text = txtConfirmar.Text.Trim();
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
