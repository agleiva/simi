using System;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento_NetDesktop.BO;
using ControlMantenimiento_NetWeb.BLL;

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
            IControlador icontrolador = Funciones.CrearControlador();
            ListaValores listavalores = (ListaValores) icontrolador.ObtenerRegistro(Funciones.ParametroBuscar,"L");
            if (listavalores != null)
            {
                lblCodigo.Text = listavalores.codigo.ToString();
                txtNombre.Text = listavalores.nombre;
                txtDescripcion.Text = listavalores.descripcion;
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
            listavalores.codigo = ElCodigo;
            listavalores.nombre = txtNombre.Text;
            listavalores.descripcion = txtDescripcion.Text;
            listavalores.tipo = Funciones.Pagina;
            IControlador icontrolador = Funciones.CrearControlador();
            Resultado = icontrolador.Guardar(listavalores);
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
