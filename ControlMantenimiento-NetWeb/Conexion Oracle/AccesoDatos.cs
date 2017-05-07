﻿/*=================================================================================================================
  Bien como se puede apreciar no se disparan Querys desde la aplicación, todo se realiza en el lado del servidor,
  lo que se traduce en mayor eficiencia y velocidad en tiempo de respuesta. Todo lo que tiene que ver con acceso
  a datos está centralizado en este módulo, que bien podría separarse aún más si se quisiera, diseñando un módulo
  DAL para cada una de las estructuras de la BD, pero para este caso se puede dejar así y funciona bastante bien
 =================================================================================================================*/

/***********************************Alternativa de Conexion con Oracle****************************************
 Si se desea conectar con oracle
 
 1. Se debe incluir la libreria: using System.Data.OracleClient;
 
 2. Se debe modificar el Web.config así:
 
    <appSettings>
      <add key="Conexion" value="Data Source=XXXX;Persist Security Info=True;User ID=XXXX;Password=XXXX;Unicode=True;" />
    </appSettings>
 
 3. Para el DAL se deben cambiar:
 
    SqlConnection <=> OracleConnection;
    SqlDataReader <=> OracleDataReader;
    SqlCommand    <=> OracleCommand;
 
 4. Adicionar a los procedimientos que devuelven datos un parametro adicional así:
 
    Cmd.Parameters.Add("Out_Data", OracleType.Cursor).Direction = ParameterDirection.Output; 
  
    En resumen reemplazar el DAL como sigue
*/

using System;
using System.Data.OracleClient;
using System.Collections;
using System.Configuration;
using System.Data;
using ControlMantenimiento_NetWeb.BO;
using ControlMantenimiento_NetWeb.BLL;

namespace ControlMantenimiento_NetWeb.DAL
{
    public class AccesoDatos
    {
        // Default Constructor
        public AccesoDatos() {}

        public static OracleConnection Cn;   // Conexion 
        public static OracleDataReader sdr;  // Cursor - Recordset de solo lectura
        public static OracleCommand Cmd;     // Objeto de tipo Command para acceder a Procedimientos Almacenados
        public static ArrayList arlListEquipo = new ArrayList();
        public static ArrayList arlListLinea = new ArrayList();
        public static ArrayList arlListMarca = new ArrayList();
        public static ArrayList arlListOperarios = new ArrayList();

       
        public static void IniciarBusqueda(string Tabla, string DatoBuscar, string Condicion)
        {
            try
            {
                Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]);
                Cmd = new OracleCommand("spr_CBuscarRegistro", Cn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                Cmd.Parameters.AddWithValue("p_DATOBUSCAR", DatoBuscar);
                Cmd.Parameters.AddWithValue("p_CONDICION", Condicion);
                Cmd.Parameters.Add("Out_Data", OracleType.Cursor).Direction = ParameterDirection.Output;                  
                Cn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static OracleDataReader BuscarRegistro(string Tabla, string DatoBuscar, string Condicion)
        {
            IniciarBusqueda(Tabla, DatoBuscar, Condicion);
            sdr = Cmd.ExecuteReader();
            return sdr; 
        }
     
        public static int ValidarTablaVacia(string Tabla)
        {
            try
            {
                using (Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new OracleCommand("spr_CValidarExistenciaDatos", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value));                   
                    LiberarRecursos();
                    return Resultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      

        
        public static ArrayList CargarListas(string Tabla, string Condicion)
        {
            try
            {
                ArrayList arlListado = new ArrayList();
                using ( Cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ToString()))
                {
                    Cmd = new SqlCommand("spr_CCargarListado", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cmd.Parameters.AddWithValue("p_CONDICION", Condicion);
                    Cn.Open();
                    using (sdr = Cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    while (sdr.Read())
                    {
                        arlListado.Add(new CargaCombosListas(sdr.GetValue(0).ToString(),  sdr.GetValue(1).ToString()));
                    }
                    sdr.Close();
                    LiberarRecursos();
                }
                return arlListado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ControlEquipos()
        {
            try
            {
                arlListEquipo = new ArrayList();
                arlListLinea = new ArrayList();
                arlListMarca = new ArrayList();
                using (Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new SqlCommand("spr_CCargarCombosListas", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", "CONTROLEQUIPOS");
                    Cn.Open();
                    using (sdr = Cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (sdr.Read())
                        {
                            if (sdr.GetValue(2).ToString().Equals("EQUIPOS"))
                            {
                                arlListEquipo.Add(new CargaCombosListas(sdr.GetValue(0).ToString(), sdr.GetValue(0).ToString() + " " + sdr.GetValue(1).ToString()));
                            }
                            else if (sdr.GetValue(2).ToString().Equals("LINEAS"))
                            {
                                arlListLinea.Add(new CargaCombosListas(sdr.GetValue(0).ToString(), sdr.GetValue(0).ToString() + " " + sdr.GetValue(1).ToString()));
                            }
                            else
                            {
                                arlListMarca.Add(new CargaCombosListas(sdr.GetValue(0).ToString(), sdr.GetValue(0).ToString() + " " + sdr.GetValue(1).ToString()));
                            }
                        }
                    sdr.Close();
                    LiberarRecursos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ControlProgramacion(string Tabla)
        {
            try
            {
                arlListEquipo = new ArrayList();
                arlListOperarios = new ArrayList();
                using (Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new SqlCommand("spr_CCargarCombosListas", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cn.Open();
                    using (sdr = Cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (sdr.Read())
                        {
                            if (sdr.GetValue(2).ToString().Equals("EQUIPOS"))
                            {
                                arlListEquipo.Add(new CargaCombosListas(sdr.GetValue(0).ToString(), sdr.GetValue(0).ToString() + " " + sdr.GetValue(1).ToString()));
                            }
                            else if (sdr.GetValue(2).ToString().Equals("OPERARIOS"))
                            {
                                arlListOperarios.Add(new CargaCombosListas(sdr.GetValue(0).ToString(), sdr.GetValue(0).ToString() + " " + sdr.GetValue(1).ToString()));
                            }
                        }
                    sdr.Close();
                    LiberarRecursos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
        /*
         =======================================================================================================================================================
         Inicio Operaciones sobre estructura Operarios
         =======================================================================================================================================================
         */

        public static Operario ObtenerOperario(string DatoBuscar, string Clave)
        {
            Operario operario = new Operario();
            try
            {

                sdr = (BuscarRegistro("OPERARIOS", DatoBuscar, Clave));
                if (sdr.Read())
                {
                    operario.documento = Convert.ToDouble(sdr["DOCUMENTO"].ToString());
                    operario.nombres = sdr["NOMBRES"].ToString();
                    operario.apellidos = sdr["APELLIDOS"].ToString();
                    operario.correo = sdr["CORREO"].ToString();
                    operario.telefono = Convert.ToDouble(sdr["TELEFONO"].ToString());
                    operario.clave = sdr["CLAVE"].ToString();
                    operario.perfil = Convert.ToInt32(sdr["PERFIL"].ToString());
                    operario.foto = sdr["FOTO"].ToString();
                    LiberarRecursos();
                    return operario;
                }
                else
                {
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GuardarOperario(Operario Operario, string Accion)
        {
            try
            {
                using ( Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new OracleCommand("spr_IUOperarios", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_ACCION", Accion);
                    Cmd.Parameters.AddWithValue("p_DOCUMENTO", Operario.documento);
                    Cmd.Parameters.AddWithValue("p_NOMBRES", Operario.nombres);
                    Cmd.Parameters.AddWithValue("p_APELLIDOS", Operario.apellidos);
                    Cmd.Parameters.AddWithValue("p_CORREO", Operario.correo);
                    Cmd.Parameters.AddWithValue("p_TELEFONO", Operario.telefono);
                    Cmd.Parameters.AddWithValue("p_CLAVE", Operario.clave);
                    Cmd.Parameters.AddWithValue("p_PERFIL", Operario.perfil);
                    Cmd.Parameters.AddWithValue("p_FOTO", Operario.foto);
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", BLL.Funciones.UsuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value));                    
                    LiberarRecursos();
                    return Resultado;
                }                
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static bool GuardarCambioClave(string NuevaClave)
        {
            bool status = false;
            try
            {
                using (Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new OracleCommand("spr_UCambioClave", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_DOCUMENTO", BLL.Funciones.UsuarioConectado);
                    Cmd.Parameters.AddWithValue("p_CLAVE", NuevaClave);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    if (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value) == 0)
                    {
                        status = true;
                    }
                    LiberarRecursos();
                }
                return status;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /*
       =======================================================================================================================================================
       Fin Operaciones sobre estructura Operarios
       =======================================================================================================================================================
     
       =======================================================================================================================================================
        Inicio Operaciones sobre estructura ListaValores
       =======================================================================================================================================================
       */
        public static ListaValores ObtenerListaValores(string DatoBuscar)
        {
            ListaValores listavalores = new ListaValores();
            try
            {

                sdr = (BuscarRegistro("LISTAVALORES", DatoBuscar, "CODIGO"));
                if (sdr.Read())
                {
                    listavalores.codigo = Convert.ToInt32(sdr["CODIGO"].ToString());
                    listavalores.nombre = sdr["NOMBRE"].ToString();
                    listavalores.descripcion = sdr["DESCRIPCION"].ToString();
                    LiberarRecursos();
                    return listavalores;
                }
                else
                {
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GuardarListaValores(ListaValores listavalores)
        {
            try
            {
                using ( Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
               
                {
                    Cmd = new OracleCommand("spr_IUListaValores", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_CODIGO", listavalores.codigo);
                    Cmd.Parameters.AddWithValue("p_NOMBRE", listavalores.nombre);
                    Cmd.Parameters.AddWithValue("p_DESCRIPCION", listavalores.descripcion);
                    Cmd.Parameters.AddWithValue("p_TIPO", listavalores.tipo);
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", BLL.Funciones.UsuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value));                    
                    LiberarRecursos();
                    return Resultado;
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /*
        =======================================================================================================================================================
        Fin Operaciones sobre estructura ListaValores
        =======================================================================================================================================================
        
        =======================================================================================================================================================
        Inicio Operaciones sobre estructura Equipos
        =======================================================================================================================================================
        */
        public static Equipo ObtenerEquipo(string DatoBuscar)
        {
            Equipo equipo = new Equipo();
            try
            {

                sdr = (BuscarRegistro("EQUIPOS", DatoBuscar, "Codigo"));
                if (sdr.Read())
                {
                    equipo.codigoequipo = Convert.ToInt32(sdr["CODIGOEQUIPO"].ToString());
                    equipo.nombreequipo = sdr["NOMBREEQUIPO"].ToString();
                    equipo.codigomarca = Convert.ToInt32(sdr["CODIGOMARCA"].ToString());
                    equipo.serie = sdr["SERIE"].ToString();
                    equipo.codigolinea = Convert.ToInt32(sdr["CODIGOLINEA"].ToString());
                    equipo.lubricacion = Convert.ToInt32(sdr["LUBRICACION"].ToString());
                    LiberarRecursos();
                    return equipo;
                }
                else
                {
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GuardarEquipo(Equipo equipo)
        {
            try
            {
                using (Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new OracleCommand("spr_IUEquipos", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_CODIGOEQUIPO", equipo.codigoequipo);
                    Cmd.Parameters.AddWithValue("p_NOMBREEQUIPO", equipo.nombreequipo);
                    Cmd.Parameters.AddWithValue("p_CODIGOMARCA", equipo.codigomarca);
                    Cmd.Parameters.AddWithValue("p_SERIE", equipo.serie);
                    Cmd.Parameters.AddWithValue("p_CODIGOLINEA", equipo.codigolinea);
                    Cmd.Parameters.AddWithValue("p_LUBRICACION", equipo.lubricacion);
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", BLL.Funciones.UsuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value));                    
                    LiberarRecursos();
                    return Resultado;
                }                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
       =======================================================================================================================================================
       Fin Operaciones sobre estructura Equipos
       =======================================================================================================================================================
       
       =======================================================================================================================================================
       Inicio Operaciones sobre estructura Mantenimiento
       =======================================================================================================================================================
       */
        public static Mantenimiento ObtenerMantenimiento(string DatoBuscar)
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            try
            {

                sdr = (BuscarRegistro("MANTENIMIENTO", DatoBuscar, ""));
                if (sdr.Read())
                {
                    mantenimiento.codigoequipo = Convert.ToInt32(sdr["CODIGOEQUIPO"].ToString());
                    mantenimiento.documento = Convert.ToDouble(sdr["DOCUMENTO"].ToString());
                    mantenimiento.fecha = Convert.ToDateTime(sdr["FECHA"].ToString());
                    mantenimiento.observaciones = sdr["OBSERVACIONES"].ToString();
                    LiberarRecursos();
                    return mantenimiento;
                }
                else
                {
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GuardarMantenimiento(Mantenimiento mantenimiento, string Accion)
        {
            try
            {
                using ( Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new OracleCommand("spr_IUMantenimiento", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_ACCION", Accion);
                    Cmd.Parameters.AddWithValue("p_CODIGOEQUIPO", mantenimiento.codigoequipo);
                    Cmd.Parameters.AddWithValue("p_DOCUMENTO", mantenimiento.documento);
                    Cmd.Parameters.AddWithValue("p_FECHA", mantenimiento.fecha);
                    Cmd.Parameters.AddWithValue("p_OBSERVACIONES", mantenimiento.observaciones);
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", BLL.Funciones.UsuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value));
                    LiberarRecursos();
                    return Resultado;
                }                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
       =======================================================================================================================================================
       Fin Operaciones sobre estructura Mantenimiento
       =======================================================================================================================================================
       */
        
        public static int EliminarRegistro(string DatoEliminar, string Tabla)
        {
            bool status = false;
            try
            {
                using ( Cn = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new OracleCommand("spr_DRegistro", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cmd.Parameters.AddWithValue("p_CONDICION", DatoEliminar);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value));
                    LiberarRecursos();
                    return Resultado;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void LiberarRecursos()
        {
            if (!sdr.IsClosed) sdr.Close();
            Cmd.Dispose();
            if (Cn != null)
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

    }
}

