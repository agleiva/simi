using System;

namespace ControlMantenimiento.Model
{
    public class Mantenimiento
    {
        public int CodigoEquipo { get; set; }

        public double Documento { get; set; }

        public DateTime Fecha { get; set; }

        public string Observaciones { get; set; }
    }
}
