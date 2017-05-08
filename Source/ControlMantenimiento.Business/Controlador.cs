using System;
using System.Collections.Generic;
using System.Text;
using ControlMantenimiento.Data;
using System.Collections;
using ControlMantenimiento.Model;

namespace ControlMantenimiento_NetDesktop.BLL
{
    public class Controlador: IControlador
    {
        private readonly IAccesoDatos _accesoDatos;
        private readonly double _usuarioConectado;

        public Controlador(IAccesoDatos accesoDatos, double usuarioConectado)
        {
            _accesoDatos = accesoDatos;
            _usuarioConectado = usuarioConectado;
        }

        #region IControlador Members

       
        public ArrayList CargarListas(string tabla)
        {
            return _accesoDatos.CargarListas(tabla);
        }

        public ArrayList CargarListas(string tabla, string condicion)
        {
            return _accesoDatos.CargarListas(tabla, condicion);
        }

        public void ControlEquipos()
        {
            _accesoDatos.ControlEquipos();
        }

        public void ControlProgramacion(string tabla)
        {
            _accesoDatos.ControlProgramacion(tabla);
        }

        public ArrayList ObtenerListaEquipos()
        {
            return _accesoDatos.ArlListEquipo;
        }

        public ArrayList ObtenerListaLineas()
        {
            return _accesoDatos.ArlListLinea;
        }

        public ArrayList ObtenerListaMarcas()
        {
            return _accesoDatos.ArlListMarca;
        }

        public ArrayList ObtenerListaOperarios()
        {
            return _accesoDatos.ArlListOperarios;
        }

        public int ValidarTablaVacia(string tabla)
        {
            return _accesoDatos.ValidarTablaVacia(tabla);
        }

        public Operario ObtenerAcceso(string datoBuscar, string clave)
        {
            Operario operario = null;
            operario = _accesoDatos.ObtenerOperario(datoBuscar, clave);
            return operario;
        }

        public Object ObtenerRegistro(string datoBuscar, string tabla)
        {
            Object objeto = null;
            switch (tabla)
            {
                case "O":
                    objeto = _accesoDatos.ObtenerOperario(datoBuscar, "");
                    break;
                case "E":
                    objeto = _accesoDatos.ObtenerEquipo(datoBuscar);
                    break;
                case "M":
                    objeto = _accesoDatos.ObtenerMantenimiento(datoBuscar);
                    break;
                case "L":
                    objeto = _accesoDatos.ObtenerListaValores(datoBuscar);
                    break;
            }
            return objeto;
        }


        // Grabar en BD
        public int Guardar(object o, string accion)
        {
            int status = 0;
            if (o is Operario)
            {
                Operario operario = (Operario)o;
                status = _accesoDatos.GuardarOperario(operario, accion, _usuarioConectado);
            }
            else if (o is Mantenimiento)
            {
                Mantenimiento mantenimiento = (Mantenimiento)o;
                status = _accesoDatos.GuardarMantenimiento(mantenimiento, accion, _usuarioConectado);
            }

            return status;
        }

        public int Guardar(object o)
        {
            int status = 0;
            if (o is Equipo)
            {
                Equipo equipo = (Equipo)o;
                status = _accesoDatos.GuardarEquipo(equipo, _usuarioConectado);
            }
            else if (o is ListaValores)
            {
                ListaValores listavalores = (ListaValores)o;
                status = _accesoDatos.GuardarListaValores(listavalores, _usuarioConectado);
            }
            return status;
        }

        public bool GuardarCambioClave(string claveNueva)
        {
            return _accesoDatos.GuardarCambioClave(claveNueva, _usuarioConectado);
        }

        // Eliminar Registro
        public int EliminarRegistro(string datoEliminar, string tabla)
        {
            return _accesoDatos.EliminarRegistro(datoEliminar, tabla);
        }


        #endregion

    }
}
