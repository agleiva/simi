using System;
using ControlMantenimiento.Business;
using ControlMantenimiento.Model;
using Funciones = ControlMantenimiento_NetWeb.BLL.Funciones;

namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmListaValores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TIPO_USUARIO"] == null)
            {
                Response.Redirect("~/Forms/wfrmAcceso.aspx");
            }
            lblTitulo.Text = Funciones.Pagina;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Funciones.ParametroBuscar != null)
            {
                LlenarCampos();
            }
        }

        private void LlenarCampos()
        {
            var controlador = Funciones.CrearControlador();
            ListaValores listavalores = (ListaValores) controlador.ObtenerRegistro(Funciones.ParametroBuscar,"L");
            if (listavalores != null)
            {
                lblCodigo.Text = listavalores.Codigo.ToString();
                txtNombre.Text = listavalores.Nombre;
                txtDescripcion.Text = listavalores.Descripcion;
            }            
            txtNombre.Focus();
        }

        private bool Verificar()
        {
            txtNombre.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtNombre.Text);
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtNombre.Focus();
                return false;
            }
            return true;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Verificar())
            {
                Guardar(Convert.ToInt32(lblCodigo.Text), (lblCodigo.Text == "0") ? Mensajes.MensajeGraba : Mensajes.MensajeActualiza);
            }
        }

        private void Guardar(int ElCodigo, string Mensaje)
        {
            int Resultado;
            ListaValores listavalores = new ListaValores();
            listavalores.Codigo = ElCodigo;
            listavalores.Nombre = txtNombre.Text;
            listavalores.Descripcion = txtDescripcion.Text;
            listavalores.Tipo = Funciones.Pagina;
            var controlador = Funciones.CrearControlador();
            Resultado = controlador.Guardar(listavalores);
            if (Resultado == 0)
            {
                listavalores = null;
                Funciones.ParametroBuscar = null;
                Response.Redirect("~/Forms/wfrmRespuesta.aspx");
            }
            else if (Resultado  == 1)
            {
              txtMensajeError.Text = Mensajes.Mensaje8;
              txtNombre.Focus();  
            }
            else
            {
                Response.Redirect("~/Forms/wfrmError.aspx");
            }
        }

        protected void ImageButtonAyuda_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
