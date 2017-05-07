using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

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

