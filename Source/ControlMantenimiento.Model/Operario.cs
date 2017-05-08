namespace ControlMantenimiento.Model
{
    public class Operario
    {
        // Default Constructor
        public Operario()
        {
        }

        public double documento { get; set; }

        public string nombres { get; set; }

        public string apellidos { get; set; }

        public string correo { get; set; }

        public double telefono { get; set; }

        public string clave { get; set; }

        public int perfil { get; set; }

        public string foto { get; set; }
    }
}
