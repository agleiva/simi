using System;
using System.Collections.Generic;
using System.Web.UI;
using ControlMantenimiento_NetWeb.BO;
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
            txtDocumento.Text = Funciones.AplicarTrim(txtDocumento.Text);
            if (string.IsNullOrEmpty(txtDocumento.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtDocumento.Focus();
                return false;
            }
            if (Funciones.Validar_SoloNumeros(txtDocumento.Text))
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
            txtNombres.Text = BLL.Funciones.EliminarTabulador(txtNombres.Text, "1MAY");
            if (Funciones.Validar_SoloLetras(txtNombres.Text))
            {
                txtMensajeError.Text = Mensajes.Mensaje23;
                txtNombres.Focus();
                return false;
            }
            txtApellidos.Text = Funciones.AplicarTrim(txtApellidos.Text);
            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtApellidos.Focus();
                return false;
            }
            txtApellidos.Text = BLL.Funciones.EliminarTabulador(txtApellidos.Text, "1MAY");
            if (Funciones.Validar_SoloLetras(txtApellidos.Text))
            {
                txtMensajeError.Text = Mensajes.Mensaje23;
                txtApellidos.Focus();
                return false;
            }
            txtCorreo.Text = Funciones.AplicarTrim(txtCorreo.Text);
            if (txtCorreo.Text.Length != 0)
            {
                txtCorreo.Text = txtCorreo.Text.ToLower();
                if (Funciones.Validar_Correo(txtCorreo.Text))
                {
                    txtMensajeError.Text = Mensajes.Mensaje16;
                    txtCorreo.Focus();
                    return false;
                }
            }
            txtTelefono.Text = Funciones.AplicarTrim(txtTelefono.Text);
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
            txtClave.Text = Funciones.AplicarTrim(txtClave.Text);
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
            operario.documento = Convert.ToInt64(txtDocumento.Text);
            operario.nombres = txtNombres.Text;
            operario.apellidos = txtApellidos.Text;
            operario.telefono = Convert.ToInt64(txtTelefono.Text);
            operario.correo = txtCorreo.Text;
            operario.clave = txtClave.Text ;
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
            operario.perfil = TipoPerfil;
            operario.foto = LaFoto;
            IControlador icontrolador = new Controlador();
            Resultado = icontrolador.Guardar(operario, Accion);
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
            IControlador icontrolador = new Controlador();           
            Operario operario = (Operario) icontrolador.ObtenerRegistro(Funciones.ParametroBuscar,"O");
            if (operario != null)
            {
                txtDocumento.Enabled = false;   
                txtDocumento.Text = operario.documento.ToString();                
                txtNombres.Text = operario.nombres;
                txtApellidos.Text = operario.apellidos;
                txtCorreo.Text = operario.correo;
                txtTelefono.Text = operario.telefono.ToString();
                txtClave.Text = operario.clave;
                TipoPerfil = operario.perfil;
                if (TipoPerfil == 3)
                {
                    dplPerfil.SelectedIndex = 0;
                }
                else
                {
                    dplPerfil.SelectedIndex = 1;
                }
                if (operario.foto != null)
                {
                    imgFoto.ImageUrl = operario.foto;
                    
                }
            }
         
            txtNombres.Focus();
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Verificar())
            {
                Guardar((txtDocumento.Enabled) ? "I" : "U", (txtDocumento.Enabled) ? BLL.Mensajes.MensajeGraba : BLL.Mensajes.MensajeActualiza);
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
