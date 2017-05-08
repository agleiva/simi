using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlMantenimiento.Business
{
    public static class Extensions
    {
        // Funcion para validar direcciones de correo electronico
        public static bool ValidarEmail(this string str) =>
            !Regex.IsMatch(str, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

        // Funcion para validar que haya solo numeros en un campo de texto
        public static bool ValidarSoloNumeros(this string str) => str.All(char.IsNumber);

        // Funcion para comprobar existencia de numeros en nombres
        public static bool ValidarSoloLetras(this string str) => !str.Any(char.IsNumber);

        // Funcion para eliminar posibles espacios de tabulacion
        public static string EliminarTabulador(this string str, string conversion)
        {
            str = str.Trim();
            while (str.IndexOf("  ", 0) != -1)
            {
                str = (str.Replace("  ", " "));
            }
            if (conversion == "MAY")
            {
                str = str.ToUpper();
            }
            if (conversion == "1MAY") // Organizar primera letra en Mayuscula y siguientes en Minuscula
            {
                str = str.ToLower();
                str = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str);
            }
            return str;
        }
    }
}
