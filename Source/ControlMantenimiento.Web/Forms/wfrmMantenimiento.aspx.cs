using System;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento_NetDesktop.BO;
using ControlMantenimiento_NetWeb.BO;
using ControlMantenimiento_NetWeb.BLL;

namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmMantenimiento : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            IControlador icontrolador = Funciones.CrearControlador();
            if (Funciones.ParametroBuscar != null)
            {
                icontrolador.ControlProgramacion("CONTROLPROGRAMACION");            
            }
            else
            {
                icontrolador.ControlProgramacion("CONTROLPROGRAMAREQUIPOS");            
            }
            dplEquipos.Items.Clear();
            dplEquipos.DataSource = icontrolador.ObtenerListaEquipos();
            dplEquipos.DataValueField = "CODIGO";     // Codigo o Documento a relacionar en BD
            dplEquipos.DataTextField = "DETALLE";     // Visualizar Nombre o Detalle           
            dplEquipos.DataBind();

            dplOperarios.Items.Clear();
            dplOperarios.DataSource = icontrolador.ObtenerListaOperarios();
            dplOperarios.DataValueField = "CODIGO";     
            dplOperarios.DataTextField = "DETALLE";                
            dplOperarios.DataBind();

            if (Funciones.ParametroBuscar != null)
            {
                LlenarCampos();
            }
        }

        private void LlenarCampos()
        {
            IControlador icontrolador = Funciones.CrearControlador();
            Mantenimiento mantenimiento =(Mantenimiento) icontrolador.ObtenerRegistro(Funciones.ParametroBuscar,"M");
            if (mantenimiento != null)
            {
                dplEquipos.SelectedValue = mantenimiento.codigoequipo.ToString();
                dplEquipos.Enabled = false;
                dplOperarios.SelectedValue = mantenimiento.documento.ToString();
                cdrFecha.SelectedDate = mantenimiento.fecha;
                cdrFecha.VisibleDate = mantenimiento.fecha;
                cdrFecha.Caption = mantenimiento.fecha.ToShortDateString();                
                txtObservaciones.Text = mantenimiento.observaciones;
            }
            
            dplOperarios.Focus();
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

        private bool Verificar()
        {
            txtObservaciones.Text = Funciones.AplicarTrim(txtObservaciones.Text);
            if (cdrFecha.SelectedDate < DateTime.Now.Date)
            {
                txtMensajeError.Text = Mensajes.Mensaje27;
                cdrFecha.Focus();
                return false;
            }
            return true;
        }

        private void Guardar()
        {
            int Resultado;
            string Accion = "U";
            Mantenimiento mantenimiento = new Mantenimiento();
            mantenimiento.codigoequipo = Convert.ToInt32(dplEquipos.SelectedValue.ToString()); 
            mantenimiento.documento = Convert.ToDouble(dplOperarios.SelectedValue.ToString()); 
            mantenimiento.fecha = Convert.ToDateTime(cdrFecha.SelectedDate);
            mantenimiento.observaciones = txtObservaciones.Text;
            if (dplEquipos.Enabled)
            {
              Accion = "I";
            }
            IControlador icontrolador = Funciones.CrearControlador();
            Resultado = icontrolador.Guardar(mantenimiento, Accion);
            if (Resultado == 0)
            {
                mantenimiento = null;
                Funciones.ParametroBuscar = null;
                Response.Redirect("~/Forms/wfrmRespuesta.aspx");
            }
            else if (Resultado == 1)
            {
              txtMensajeError.Text = Mensajes.Mensaje10;
              dplOperarios.Focus();                        
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
