using System;


namespace ControlMantenimiento_NetWeb.BO
{
    public class Mantenimiento
    {
        private int      CodigoEquipo;
        private double   Documento;
        private DateTime Fecha;
        private string   Observaciones;

        public int codigoequipo
        {
            get { return CodigoEquipo; }
            set { CodigoEquipo = value; }
        }

        public double documento
        {
            get { return Documento; }
            set { Documento = value; }
        }

        public DateTime fecha
        {
            get { return Fecha; }
            set { Fecha = value; }
        }

        public string observaciones
        {
            get { return Observaciones; }
            set { Observaciones = value; }
        }

        // Default Constructor
        public Mantenimiento()
        {
        }
    }
}
