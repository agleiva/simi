using System;

namespace ControlMantenimiento_NetWeb.BO
{
    public class Operario
    {
        private double Documento;
        private string Nombres;
        private string Apellidos;
        private string Correo;
        private double Telefono;
        private string Clave;
        private int Perfil;
        private string Foto;

        public double documento
        {
            get { return Documento; }
            set { Documento = value; }
        }
        
        public string nombres
        {
            get { return Nombres; }
            set { Nombres = value; }
        }
        
        public string apellidos
        {
            get { return Apellidos; }
            set { Apellidos = value; }
        }
        
        public string correo
        {
            get { return Correo; }
            set { Correo = value; }
        }
        
        public double telefono
        {
            get { return Telefono; }
            set { Telefono = value; }
        }
        public string clave
        {
            get { return Clave; }
            set { Clave = value; }
        }
        public int perfil
        {
            get { return Perfil; }
            set { Perfil = value; }
        }
        public string foto
        {
            get { return Foto; }
            set { Foto = value; }
        }
        
        
        // Default Constructor
        public Operario()
        {
        }
    }
}
