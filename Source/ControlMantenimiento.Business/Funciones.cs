using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlMantenimiento.Business
{
    public class Funciones
    {
        // Funcion para eliminar posibles espacios en blanco
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

        // Funcion para validar que haya solo numeros en un campo de texto
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

        // Funcion para comprobar existencia de numeros en nombres
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

        // Funcion para eliminar posibles espacios de tabulacion
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
            if (Conversion == "1MAY") // Organizar primera letra en Mayuscula y siguientes en Minuscula
            {
                Cadena = Cadena.ToLower();
                Cadena = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Cadena);
            }
            return Cadena;
        }
    }
}
