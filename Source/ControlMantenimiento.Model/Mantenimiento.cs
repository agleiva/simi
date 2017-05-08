using System;



namespace ControlMantenimiento.Model
{
    public class Mantenimiento
    {
        private int      CodigoEquipo;
        private double   Documento;
        private DateTime Fecha;
        private string   Observaciones;

       // Default Constructor
       public Mantenimiento()
       {
       }

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

       
    }
}
