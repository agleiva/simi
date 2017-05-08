using System;
using System.Web.UI.WebControls;
using ControlMantenimiento_NetWeb.BLL;
using ControlMantenimiento_NetWeb.Forms;


namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TIPO_USUARIO"] == null)
            {
                Response.Redirect("~/Forms/wfrmAcceso.aspx");
            }
            if (Funciones.PerfilAcceso != 1)
            {              
                LinkButton2.Enabled = false;
                LinkButton3.Enabled = false;
                LinkButton4.Enabled = false;               
                LinkButton5.Enabled = false;                
            }
            Funciones.ParametroBuscar = null;
            lblUsuarioConectado.Text ="Bienvenido: " + " "+ Funciones.NombreUsuario;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (Funciones.PerfilAcceso != 1)
            {
                Funciones.ParametroBuscar = Convert.ToString(Funciones.UsuarioConectado);
                Response.Redirect("~/Forms/wfrmOperarios.aspx");
            }
            else
            {
                Funciones.Pagina = "OPERARIOS";
                Response.Redirect("~/Forms/wfrmBusquedas.aspx");
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Funciones.Pagina = "EQUIPOS";
            Response.Redirect("~/Forms/wfrmBusquedas.aspx");
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Funciones.Pagina = "MARCAS";
            Response.Redirect("~/Forms/wfrmBusquedas.aspx");
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Funciones.Pagina = "LINEAS";
            Response.Redirect("~/Forms/wfrmBusquedas.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Funciones.Pagina = "MANTENIMIENTO";
            Response.Redirect("~/Forms/wfrmBusquedas.aspx");
        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/wfrmCambioClave.aspx");
        }

        

        
    }
}
