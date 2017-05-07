using System;
using System.Collections.Generic;
using System.Text;
using ControlMantenimiento_NetDesktop.DAL;
using System.Collections;
using ControlMantenimiento_NetDesktop.BO;

namespace ControlMantenimiento_NetDesktop.BLL
{
    public class Controlador: IControlador
    {
        public Controlador() { }

        #region IControlador Members

       
        public ArrayList CargarListas(string Tabla)
        {
            return AccesoDatos.CargarListas(Tabla);
        }

        public ArrayList CargarListas(string Tabla, string Condicion)
        {
            return AccesoDatos.CargarListas(Tabla, Condicion);
        }

        public void ControlEquipos()
        {
            AccesoDatos.ControlEquipos();
        }

        public void ControlProgramacion(string Tabla)
        {
            AccesoDatos.ControlProgramacion(Tabla);
        }

        public ArrayList ObtenerListaEquipos()
        {
            return AccesoDatos.arlListEquipo;
        }

        public ArrayList ObtenerListaLineas()
        {
            return AccesoDatos.arlListLinea;
        }

        public ArrayList ObtenerListaMarcas()
        {
            return AccesoDatos.arlListMarca;
        }

        public ArrayList ObtenerListaOperarios()
        {
            return AccesoDatos.arlListOperarios;
        }

        public int ValidarTablaVacia(string Tabla)
        {
            return AccesoDatos.ValidarTablaVacia(Tabla);
        }

        public Operario ObtenerAcceso(string DatoBuscar, string Clave)
        {
            Operario operario = null;
            operario = AccesoDatos.ObtenerOperario(DatoBuscar, Clave);
            return operario;
        }

        public Object ObtenerRegistro(string DatoBuscar, string Tabla)
        {
            Object objeto = null;
            switch (Tabla)
            {
                case "O":
                    objeto = AccesoDatos.ObtenerOperario(DatoBuscar, "");
                    break;
                case "E":
                    objeto = AccesoDatos.ObtenerEquipo(DatoBuscar);
                    break;
                case "M":
                    objeto = AccesoDatos.ObtenerMantenimiento(DatoBuscar);
                    break;
                case "L":
                    objeto = AccesoDatos.ObtenerListaValores(DatoBuscar);
                    break;
            }
            return objeto;
        }


        // Grabar en BD
        public int Guardar(object o, string Accion)
        {
            int status = 0;
            if (o is Operario)
            {
                Operario operario = (Operario)o;
                status = AccesoDatos.GuardarOperario(operario, Accion);
            }
            else if (o is Mantenimiento)
            {
                Mantenimiento mantenimiento = (Mantenimiento)o;
                status = AccesoDatos.GuardarMantenimiento(mantenimiento, Accion);
            }

            return status;
        }

        public int Guardar(object o)
        {
            int status = 0;
            if (o is Equipo)
            {
                Equipo equipo = (Equipo)o;
                status = AccesoDatos.GuardarEquipo(equipo);
            }
            else if (o is ListaValores)
            {
                ListaValores listavalores = (ListaValores)o;
                status = AccesoDatos.GuardarListaValores(listavalores);
            }
            return status;
        }

        public bool GuardarCambioClave(string ClaveNueva)
        {
            return AccesoDatos.GuardarCambioClave(ClaveNueva);
        }

        // Eliminar Registro
        public int EliminarRegistro(string DatoEliminar, string Tabla)
        {
            return AccesoDatos.EliminarRegistro(DatoEliminar, Tabla);
        }


        #endregion

    }
}
