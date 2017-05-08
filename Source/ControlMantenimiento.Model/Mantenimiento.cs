using System;

namespace ControlMantenimiento.Model
{
    public class Mantenimiento
    {
        // Default Constructor
       public Mantenimiento()
       {
       }

       public int codigoequipo { get; set; }

        public double documento { get; set; }

        public DateTime fecha { get; set; }

        public string observaciones { get; set; }
    }
}
