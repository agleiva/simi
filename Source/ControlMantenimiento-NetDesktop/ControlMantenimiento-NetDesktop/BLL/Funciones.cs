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

        // Funcion para eliminar posibles espacios en blanco al inicio y fin de una cadena de texto
        public static string AplicarTrim(string Cadena)
        {            
            Cadena = Cadena.Trim(); 
            return Cadena;
        }

        // Funcion para validar direcciones de correo electronico
        public static bool Validar_Correo(string Cadena)
        {
          bool status = false;
          string Expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
          if (!Regex.IsMatch(Cadena, Expresion))
          {
              status = true;
          }
          return status;
        }

        // Funcion para comprobar existencia de caracteres no numericos
        public static bool Validar_SoloNumeros(string Cadena)
        {
            bool status = false;
            for (int i = 0; i < Cadena.Length; i++)
            {
                if (!Char.IsNumber(Cadena[i]))
                {
                    status = true;
                    break;
                }
            }
            return status;
        }

        // Funcion para comprobar existencia de caracteres diferentes de letras
        public static bool Validar_SoloLetras(string Cadena)
        {
            bool Resultado = false;
            for (int i = 0; i < Cadena.Length; i++)
            {
                if (Char.IsNumber(Cadena[i]))
                {
                    Resultado = true;
                    break;
                }
            }
            return Resultado;
        }

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

        // Funcion para eliminar posibles espacios de tabulacion en un campo de texto
        public static string EliminarTabulador(string Cadena, string Conversion)
        {
            Cadena = Cadena.Trim();
            while (Cadena.IndexOf("  ", 0) != -1)
            {
                Cadena = (Cadena.Replace("  ", " "));
            }
            if (Conversion == "MAY")
            {
                Cadena = Cadena.ToUpper();
            }
            else if (Conversion == "1MAY") // Organizar primera letra en Mayuscula y siguientes en Minuscula
            {
                Cadena = Cadena.ToLower();
                Cadena = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Cadena);
            }
            return Cadena;
        }
        
        // Default Constructor
        public Funciones()
        { }
    }
}
