using System;



namespace ControlMantenimiento.Model
{
    public class ListaValores
    {
       private int    Codigo;
       private string Nombre;
       private string Descripcion;
       private string Tipo;

       // Default Constructor
       public ListaValores()
       {
       }

       public int codigo
       {
           get { return Codigo; }
           set { Codigo = value; }
       }

        public string  nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        public string descripcion
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }

        public string tipo
        {
            get { return Tipo; }
            set { Tipo = value; }
        }

    }

    }

