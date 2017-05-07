using System;

namespace ControlMantenimiento_NetWeb.BLL
{
    public class CargaCombosListas
    {
        private string Codigo;
        private string Detalle;

        public string codigo
        {
            get { return Codigo; }
            set { Codigo = value; }
        }

        public string detalle
        {
            get { return Detalle; }
            set { Detalle = value; }
        }

        // Default Constructor
        public CargaCombosListas()
        {
        }

        public CargaCombosListas(string codigo, string detalle)
        {
            Codigo = codigo;
            Detalle = detalle;
        }
    }
}
