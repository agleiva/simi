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
        private readonly AccesoDatos _accesoDatos;

        public Controlador(AccesoDatos accesoDatos)
        {
            _accesoDatos = accesoDatos;
        }

        #region IControlador Members

       
        public ArrayList CargarListas(string Tabla)
        {
            return _accesoDatos.CargarListas(Tabla);
        }

        public ArrayList CargarListas(string Tabla, string Condicion)
        {
            return _accesoDatos.CargarListas(Tabla, Condicion);
        }

        public void ControlEquipos()
        {
            _accesoDatos.ControlEquipos();
        }

        public void ControlProgramacion(string Tabla)
        {
            _accesoDatos.ControlProgramacion(Tabla);
        }

        public ArrayList ObtenerListaEquipos()
        {
            return _accesoDatos.arlListEquipo;
        }

        public ArrayList ObtenerListaLineas()
        {
            return _accesoDatos.arlListLinea;
        }

        public ArrayList ObtenerListaMarcas()
        {
            return _accesoDatos.arlListMarca;
        }

        public ArrayList ObtenerListaOperarios()
        {
            return _accesoDatos.arlListOperarios;
        }

        public int ValidarTablaVacia(string Tabla)
        {
            return _accesoDatos.ValidarTablaVacia(Tabla);
        }

        public Operario ObtenerAcceso(string DatoBuscar, string Clave)
        {
            Operario operario = null;
            operario = _accesoDatos.ObtenerOperario(DatoBuscar, Clave);
            return operario;
        }

        public Object ObtenerRegistro(string DatoBuscar, string Tabla)
        {
            Object objeto = null;
            switch (Tabla)
            {
                case "O":
                    objeto = _accesoDatos.ObtenerOperario(DatoBuscar, "");
                    break;
                case "E":
                    objeto = _accesoDatos.ObtenerEquipo(DatoBuscar);
                    break;
                case "M":
                    objeto = _accesoDatos.ObtenerMantenimiento(DatoBuscar);
                    break;
                case "L":
                    objeto = _accesoDatos.ObtenerListaValores(DatoBuscar);
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
                status = _accesoDatos.GuardarOperario(operario, Accion);
            }
            else if (o is Mantenimiento)
            {
                Mantenimiento mantenimiento = (Mantenimiento)o;
                status = _accesoDatos.GuardarMantenimiento(mantenimiento, Accion);
            }

            return status;
        }

        public int Guardar(object o)
        {
            int status = 0;
            if (o is Equipo)
            {
                Equipo equipo = (Equipo)o;
                status = _accesoDatos.GuardarEquipo(equipo);
            }
            else if (o is ListaValores)
            {
                ListaValores listavalores = (ListaValores)o;
                status = _accesoDatos.GuardarListaValores(listavalores);
            }
            return status;
        }

        public bool GuardarCambioClave(string ClaveNueva)
        {
            return _accesoDatos.GuardarCambioClave(ClaveNueva);
        }

        // Eliminar Registro
        public int EliminarRegistro(string DatoEliminar, string Tabla)
        {
            return _accesoDatos.EliminarRegistro(DatoEliminar, Tabla);
        }


        #endregion

    }
}
