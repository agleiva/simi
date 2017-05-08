using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ControlMantenimiento.Model;


namespace ControlMantenimiento_NetDesktop.BLL
{
    public interface IControlador
    {
        // Validar existencia de registros en tabla implicada
        int ValidarTablaVacia(string Tabla);

        // Carga de Listas
        void ControlProgramacion(string Tabla);
        void ControlEquipos();
        ArrayList ObtenerListaEquipos();
        ArrayList ObtenerListaLineas();
        ArrayList ObtenerListaMarcas();
        ArrayList CargarListas(string Tabla);
        ArrayList CargarListas(string Tabla, string Condicion);
        ArrayList ObtenerListaOperarios();
          
        // Obtener Registro
        Operario ObtenerAcceso(string DatoBuscar, string Clave);
        Object ObtenerRegistro(string DatoBuscar, string Tabla);

        // Grabar en BD
        int Guardar(object o, string Accion);
        int Guardar(object o);
        bool GuardarCambioClave(string ClaveNueva);
       
        //Eliminar Registro
        int EliminarRegistro(string DatoEliminar, string Tabla);
    }
}
