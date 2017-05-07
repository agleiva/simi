using System;



namespace ControlMantenimiento_NetDesktop.BO
{
    public class Equipo
    {

        private int     CodigoEquipo;
        private string  NombreEquipo;
        private int     CodigoMarca;
        private string  Serie;
        private int     CodigoLinea;
        private int     Lubricacion;


        // Default Constructor
        public Equipo() { }

        public int codigoequipo
        {
            get { return CodigoEquipo; }
            set { CodigoEquipo = value; }
        }

        public string nombreequipo
        {
            get { return NombreEquipo; }
            set { NombreEquipo = value; }
        }

        public int codigomarca
       {
           get { return CodigoMarca; }
           set { CodigoMarca = value; }
       }

       public string serie
       {
           get { return Serie; }
           set { Serie = value; }
       }

       public int codigolinea
       {
           get { return CodigoLinea; }
           set { CodigoLinea = value; }
       }

       public int lubricacion
       {
           get { return Lubricacion; }
           set { Lubricacion = value; }
       }
        
    }
}
