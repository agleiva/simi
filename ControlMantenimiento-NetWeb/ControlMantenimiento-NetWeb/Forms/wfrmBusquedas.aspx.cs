using System;
using ControlMantenimiento_NetWeb.BLL;
using ControlMantenimiento_NetWeb.Forms;
using System.Web.UI.WebControls;

namespace ControlMantenimiento_NetWeb.Forms
{
    public partial class wfrmBusquedas : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TIPO_USUARIO"] == null)
            {
                Response.Redirect("~/Forms/wfrmAcceso.aspx");
            }
            if (!this.IsPostBack)
            {
                this.BindRepeater();
            }
        }

        private void BindRepeater()
        {
            IControlador icontrolador = new Controlador();
            Repeater1.DataSource = icontrolador.CargarListas(Funciones.Pagina, "");
            Repeater1.DataBind();
        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = txtBuscar.Text.Trim();
            LabelInformacion.Text = "";
            LabelInformacion.ForeColor = System.Drawing.Color.Red;
            if (string.IsNullOrEmpty(txtBuscar.Text.Trim()))
            {
                LabelInformacion.Text = Mensajes.MensajeCampoRequerido;
                txtBuscar.Focus();
            }
            else if (txtBuscar.Text.Substring(0, 1) == "0")
            {
                LabelInformacion.Text = Mensajes.Mensaje6;
                txtBuscar.Focus();
            }
            else
            {
                IControlador icontrolador = new Controlador();
                Repeater1.DataSource = icontrolador.CargarListas(Funciones.Pagina, txtBuscar.Text);
                Repeater1.DataBind();
            }

        }

        

        protected void Repeater1_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                switch (Funciones.Pagina)
                {
                    case "OPERARIOS":
                        Funciones.ParametroBuscar = ((Button)e.CommandSource).CommandArgument;
                        Response.Redirect("~/Forms/wfrmOperarios.aspx");
                        break;
                    case "EQUIPOS":
                        Funciones.ParametroBuscar = ((Button)e.CommandSource).CommandArgument;
                        Response.Redirect("~/Forms/wfrmEquipos.aspx");
                        break;
                    case "MARCAS":
                        Funciones.ParametroBuscar = ((Button)e.CommandSource).CommandArgument;
                        Response.Redirect("~/Forms/wfrmListavalores.aspx");
                        break;
                    case "LINEAS":
                        Funciones.ParametroBuscar = ((Button)e.CommandSource).CommandArgument;
                        Response.Redirect("~/Forms/wfrmListavalores.aspx");
                        break;
                    case "MANTENIMIENTO":
                        Funciones.ParametroBuscar = ((Button)e.CommandSource).CommandArgument;
                        Response.Redirect("~/Forms/wfrmMantenimiento.aspx");
                        break;
                }
            }
            else if (e.CommandName == "Eliminar")
            {                                
                    int Resultado;
                    IControlador icontrolador = new Controlador();
                    if (Funciones.Pagina.Equals("MARCAS") || Funciones.Pagina.Equals("LINEAS"))
                    {
                        Resultado = icontrolador.EliminarRegistro(((Button)e.CommandSource).CommandArgument, "LISTAVALORES"); 
                    }
                    else
                    {
                        Resultado = icontrolador.EliminarRegistro(((Button)e.CommandSource).CommandArgument, Funciones.Pagina);
 
                    }
                    if (Resultado == 0)
                    {                       
                        Response.Redirect("~/Forms/wfrmBusquedas.aspx");                        
                    }
                    else if (Resultado == 1)
                    {
                        if (Funciones.Pagina.Equals("OPERARIOS"))
                        {
                            LabelInformacion.Text = Mensajes.Mensaje20;
                        }
                        else if (Funciones.Pagina.Equals("EQUIPOS"))
                        {
                            LabelInformacion.Text = Mensajes.Mensaje22;
                        }
                        else
                        {
                            LabelInformacion.Text = Mensajes.Mensaje9;
                        }
                    }
                    else
                    {
                        LabelInformacion.Text = Mensajes.MensajeErrorBD;      
                        Response.Redirect("~/Forms/wfrmError.aspx");
                    }                      
                }
            
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            int Resultado;
            IControlador icontrolador = new Controlador();
            switch (Funciones.Pagina)
            {
                case "OPERARIOS":
                    Response.Redirect("~/Forms/wfrmOperarios.aspx");
                    break;
                case "EQUIPOS":
                    Resultado = icontrolador.ValidarTablaVacia("CMARCAS");
                    if (Resultado == -1)
                    {
                        Funciones.MensajeError = Mensajes.Mensaje11;
                        Response.Redirect("~/Forms/wfrmError.aspx");
                    }

                    else if (Resultado == -2)
                    {
                        Funciones.MensajeError = Mensajes.Mensaje12;
                        Response.Redirect("~/Forms/wfrmError.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Forms/wfrmEquipos.aspx");
                    }
                    break;
                case "MARCAS":
                    Response.Redirect("~/Forms/wfrmListavalores.aspx");
                    break;
                case "LINEAS":
                    Response.Redirect("~/Forms/wfrmListavalores.aspx");
                    break;
                case "MANTENIMIENTO":
                    Resultado = icontrolador.ValidarTablaVacia("CPROGRAMAREQUIPOS");
                    if (Resultado == -1) 
                    {
                      Funciones.MensajeError = Mensajes.Mensaje14;
                      Response.Redirect("~/Forms/wfrmError.aspx");
                    }
                    else if (Resultado == -2)
                    {
                        Funciones.MensajeError = Mensajes.Mensaje13;
                        Response.Redirect("~/Forms/wfrmError.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Forms/wfrmMantenimiento.aspx");
                    }
                    break;
            }
        }
    }
}
