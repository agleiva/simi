using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using ControlMantenimiento.Business;
using ControlMantenimiento.Data;

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

        public static Controlador CrearControlador()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["Conexion"];
            var accesoDatos = AccesoDatosFactory.GetAccesoDatos(connectionStringSettings.ConnectionString, connectionStringSettings.ProviderName);
            return new Controlador(accesoDatos, UsuarioConectado);
        }

        // Default Constructor
        public Funciones()
        { }
    }
}
