using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento_NetDesktop.DAL;

namespace ControlMantenimiento_NetWeb.BLL
{
    public class Funciones
    {
        public static string Pagina = null;
        public static string ParametroBuscar = null;        
        public static double UsuarioConectado;
        public static int    PerfilAcceso;
        public static string NombreUsuario;
        public static string MensajeError;


        public static Controlador CrearControlador()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ToString();
            return new Controlador(new AccesoDatos(connectionString), UsuarioConectado);
        }


        // Funcion para limpiar los controles en un formulario (Solo TextBox)
        public static void LimpiarForma(Control strWebForm)    
     {
         
         foreach (Control strControl in strWebForm.Controls)
         {
             if (strControl.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
             {
                 ((TextBox)strControl).Text = string.Empty;
             }
         }
     }

        

        

        public Funciones()
        { }
    }
}

