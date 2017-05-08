namespace ControlMantenimiento.Model
{
    public class CargaCombosListas
    {
        public string codigo { get; set; }

        public string detalle { get; set; }

        // Default Constructor
       public CargaCombosListas()
       {
       }

       public CargaCombosListas(string codigo, string detalle)
       {
           this.codigo  = codigo;
           this.detalle = detalle;
       }
    }  
}
