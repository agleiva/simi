using System;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento.Model;
using ControlMantenimiento_NetWeb.BLL;

namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmEquipos : System.Web.UI.Page
    {
        
        protected void Page_Init(object sender, EventArgs e)
        {
            IControlador icontrolador = Funciones.CrearControlador();
            icontrolador.ControlEquipos();
            dplMarcas.DataValueField = "CODIGO";
            dplMarcas.DataTextField = "DETALLE";
            dplMarcas.DataSource = icontrolador.ObtenerListaMarcas();
            dplMarcas.DataBind();

            dplLineas.DataValueField = "CODIGO";
            dplLineas.DataTextField = "DETALLE";
            dplLineas.DataSource = icontrolador.ObtenerListaLineas();
            dplLineas.DataBind();

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

        private void LlenarCampos()
        {
            IControlador icontrolador = Funciones.CrearControlador();
            Equipo equipo = (Equipo)icontrolador.ObtenerRegistro(Funciones.ParametroBuscar,"E");
            if (equipo != null)
            {
                lblCodigo.Text = equipo.codigoequipo.ToString();                
                dplMarcas.SelectedValue = equipo.codigomarca.ToString();
                txtNombre.Text = equipo.nombreequipo;
                txtSerie.Text = equipo.serie;
                dplLineas.SelectedValue = equipo.codigolinea.ToString();
                if (equipo.lubricacion == 1)
                {
                    chkLubricacion.Checked = true;
                }
                else
                {
                    chkLubricacion.Checked = false;
                }
            }
         
            txtNombre.Focus();
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
            Equipo equipo = new Equipo();
            equipo.codigoequipo = ElCodigo;
            equipo.nombreequipo = txtNombre.Text;            
            equipo.codigomarca = Convert.ToInt32(dplMarcas.SelectedValue);
            equipo.codigolinea = Convert.ToInt32(dplLineas.SelectedValue);
            equipo.serie = txtSerie.Text;
            if (chkLubricacion.Checked)
            {
                equipo.lubricacion = 1;
            }
            else
            {
                equipo.lubricacion = 0;
            }
            IControlador icontrolador = Funciones.CrearControlador();
            Resultado = icontrolador.Guardar(equipo);
            if (Resultado == 0)
            {
                equipo = null;
                Funciones.ParametroBuscar = null;
                Response.Redirect("~/Forms/wfrmRespuesta.aspx");
            }
            else if (Resultado == 1)
            {
                txtMensajeError.Text = Mensajes.Mensaje7;
                txtSerie.Focus(); 
            }
            else
            {
                Response.Redirect("~/Forms/wfrmError.aspx");
            }

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
            txtSerie.Text = ControlMantenimiento.Business.Funciones.AplicarTrim(txtSerie.Text);
            if (string.IsNullOrEmpty(txtSerie.Text))
            {
                txtMensajeError.Text = Mensajes.MensajeCampoRequerido;
                txtSerie.Focus();
                return false;
            }           
            return true;
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
