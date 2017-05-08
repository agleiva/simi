using System.Collections;
using ControlMantenimiento.Model;

namespace ControlMantenimiento.Data
{
    public interface IAccesoDatos
    {
        int ValidarTablaVacia(string tabla);
        ArrayList CargarListas(string tabla);
        ArrayList CargarListas(string tabla, string condicion);
        void ControlEquipos();
        void ControlProgramacion(string tabla);
        Operario ObtenerOperario(string datoBuscar, string clave);
        int GuardarOperario(Operario operario, string accion, double usuarioConectado);
        bool GuardarCambioClave(string nuevaClave, double usuarioConectado);
        ListaValores ObtenerListaValores(string datoBuscar);
        int GuardarListaValores(ListaValores listavalores, double usuarioConectado);
        Equipo ObtenerEquipo(string datoBuscar);
        int GuardarEquipo(Equipo equipo, double usuarioConectado);
        Mantenimiento ObtenerMantenimiento(string datoBuscar);
        int GuardarMantenimiento(Mantenimiento mantenimiento, string accion, double usuarioConectado);
        int EliminarRegistro(string datoEliminar, string tabla);

        ArrayList ArlListEquipo { get; }
        ArrayList ArlListOperarios { get; }
        ArrayList ArlListLinea { get; }
        ArrayList ArlListMarca { get; }
    }
}