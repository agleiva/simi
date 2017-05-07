using System;

using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ControlMantenimiento_NetDesktop.BLL
{
    class Funciones
    {
        public static double UsuarioConectado; // Documento de Usuario conectado actualmente
        public static int    PerfilAcceso;     // Perfil de permisos sobre el sistema
        public static string ValorTipo;        // Variable para controlar Tipo en Lista de Valores (Lineas o Marcas)
        public static string NombreUsuario;
        public static string Fuente;

      

        

        

        

        // Funcion para limpiar los controles en un formulario (Solo TextBox)
        public static void LimpiarForma(Panel pnl)
        {
            foreach (Control oControls in pnl.Controls)
            {
                if (oControls is TextBox)
                {
                    oControls.Text = ""; 
                }
            }
        }

        
        
        // Default Constructor
        public Funciones()
        { }
    }
}
