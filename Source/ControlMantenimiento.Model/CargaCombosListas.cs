namespace ControlMantenimiento.Model
{
    public class CargaCombosListas
    {
        public string Codigo { get; set; }

        public string Detalle { get; set; }

        public CargaCombosListas(string codigo, string detalle)
        {
            this.Codigo = codigo;
            this.Detalle = detalle;
        }
    }
}
