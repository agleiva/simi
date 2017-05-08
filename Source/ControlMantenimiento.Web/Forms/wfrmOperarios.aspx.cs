using System;
using System.Collections.Generic;
using System.Web.UI;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento.Model;
using ControlMantenimiento_NetWeb.BLL;

namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmOperarios : System.Web.UI.Page
    {
        private int TipoPerfil;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Funciones.PerfilAcceso != 1)
            {
                dplPerfil.Enabled = false;
            }
            if (Funciones.ParametroBuscar != null)
            {
                LlenarCampos();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TIPO_USUARIO"] == null)
            {
                Response.Redirect("~/Forms/wfrmAcceso.aspx");
            }
        }
        
        private string LaFoto = "";

        private bool Verificar()
        {
            txtDocumento.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtDocumento.Text);
            if (string.IsNullOrEmpty(txtDocumento.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtDocumento.Focus();
                return false;
            }
            if (ControlMantenimiento.Business.Funciones.Validar_SoloNumeros(txtDocumento.Text))
            {
                txtMensajeError.Text = Mensajes.Mensaje25;
                txtDocumento.Focus();
                return false;
            }
            if (txtDocumento.Text.Length < 6)
            {
                txtMensajeError.Text = Mensajes.Mensaje15;
                txtDocumento.Focus();
                return false;
            }
            if (txtDocumento.Text.Substring(0, 1) == "0")
            {
                txtMensajeError.Text = Mensajes.Mensaje6;
                txtDocumento.Focus();
                return false;
            }            
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtNombres.Focus();
                return false;
            }
            txtNombres.Text = ControlMantenimiento.Business.Funciones.EliminarTabulador(txtNombres.Text, "1MAY");
            if (ControlMantenimiento.Business.Funciones.Validar_SoloLetras(txtNombres.Text))
            {
                txtMensajeError.Text = Mensajes.Mensaje23;
                txtNombres.Focus();
                return false;
            }
            txtApellidos.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtApellidos.Text);
            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtApellidos.Focus();
                return false;
            }
            txtApellidos.Text = ControlMantenimiento.Business.Funciones.EliminarTabulador(txtApellidos.Text, "1MAY");
            if (ControlMantenimiento.Business.Funciones.Validar_SoloLetras(txtApellidos.Text))
            {
                txtMensajeError.Text = Mensajes.Mensaje23;
                txtApellidos.Focus();
                return false;
            }
            txtCorreo.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtCorreo.Text);
            if (txtCorreo.Text.Length != 0)
            {
                txtCorreo.Text = txtCorreo.Text.ToLower();
                if (ControlMantenimiento.Business.Funciones.Validar_Correo(txtCorreo.Text))
                {
                    txtMensajeError.Text = Mensajes.Mensaje16;
                    txtCorreo.Focus();
                    return false;
                }
            }
            txtTelefono.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtTelefono.Text);
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtTelefono.Focus();
                return false;
            }
            if ((txtTelefono.Text.Length != 7) && (txtTelefono.Text.Length != 10))
            {
                txtMensajeError.Text = Mensajes.Mensaje17;
                txtTelefono.Focus();
                return false;
            }
            if (txtTelefono.Text.Substring(0, 1) == "0")
            {
                txtMensajeError.Text = Mensajes.Mensaje6;
                txtTelefono.Focus();
                return false;
            }
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
        
            return true;
        }

        private void Guardar(string Accion, string Mensaje)
        {
            int Resultado;
            Operario operario = new Operario();
            operario.Documento = Convert.ToInt64(txtDocumento.Text);
            operario.Nombres = txtNombres.Text;
            operario.Apellidos = txtApellidos.Text;
            operario.Telefono = Convert.ToInt64(txtTelefono.Text);
            operario.Correo = txtCorreo.Text;
            operario.Clave = txtClave.Text ;
            if (TipoPerfil != 1) // Proteger Perfil de Administrador
            {
                if (dplPerfil.Text == "3")
                {
                    TipoPerfil = 3;
                }
                else
                {
                    TipoPerfil = 2;
                }
            }
            operario.Perfil = TipoPerfil;
            operario.Foto = LaFoto;
            var controlador = Funciones.CrearControlador();
            Resultado = controlador.Guardar(operario, Accion);
            if (Resultado == 0)
            {
               operario = null;
               Funciones.ParametroBuscar = null;
               Response.Redirect("~/Forms/wfrmRespuesta.aspx");
            }
            else if (Resultado == 1)
            {
              txtMensajeError.Text = Mensajes.Mensaje26;
              txtDocumento.Focus();                   
            }
            else
            {
               Response.Redirect("~/Forms/wfrmError.aspx");
            }
        }

        private void Limpiar()
        {
            Control strWebForm = Page.FindControl("frmOperarios");
            Funciones.LimpiarForma(strWebForm);            
        }
        
        private void LlenarCampos()
        {
            var controlador = Funciones.CrearControlador();
            Operario operario = (Operario) controlador.ObtenerRegistro(Funciones.ParametroBuscar,"O");
            if (operario != null)
            {
                txtDocumento.Enabled = false;   
                txtDocumento.Text = operario.Documento.ToString();                
                txtNombres.Text = operario.Nombres;
                txtApellidos.Text = operario.Apellidos;
                txtCorreo.Text = operario.Correo;
                txtTelefono.Text = operario.Telefono.ToString();
                txtClave.Text = operario.Clave;
                TipoPerfil = operario.Perfil;
                if (TipoPerfil == 3)
                {
                    dplPerfil.SelectedIndex = 0;
                }
                else
                {
                    dplPerfil.SelectedIndex = 1;
                }
                if (operario.Foto != null)
                {
                    imgFoto.ImageUrl = operario.Foto;
                    
                }
            }
         
            txtNombres.Focus();
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Verificar())
            {
                Guardar((txtDocumento.Enabled) ? "I" : "U", (txtDocumento.Enabled) ? Mensajes.MensajeGraba : Mensajes.MensajeActualiza);
            }  
        }


        protected void ImageButtonAyuda_Click(object sender, ImageClickEventArgs e)
        {
            /* System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "E:/Fuentes CM/ControlMantenimiento-NetWeb/ControlMantenimiento-NetWeb/ControlMantenimiento-NetWeb/Ayudas/Ayuda.chm";
            proc.Start();
            proc.Dispose();*/

            // Estas líneas son las encargadas de llamar el archivo de ayudas .chm, está en comentario para que usted le coloque la ruta
            // donde descomprimió el archivo descargado de la web
        }
      
                     
    }    
}
