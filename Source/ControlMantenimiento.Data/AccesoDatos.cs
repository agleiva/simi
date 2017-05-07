/*=================================================================================================================
  Bien como se puede apreciar no se disparan Querys desde la aplicación, todo se realiza en el lado del servidor,
  lo que se traduce en mayor eficiencia y velocidad en tiempo de respuesta. Todo lo que tiene que ver con acceso
  a datos está centralizado en este módulo, que bien podría separarse aún más si se quisiera, diseñando un módulo
  DAL para cada una de las estructuras de la BD, pero para este caso se puede dejar así y funciona bastante bien
 =================================================================================================================*/

using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using ControlMantenimiento_NetDesktop.BLL;
using ControlMantenimiento_NetDesktop.BO;

namespace ControlMantenimiento_NetDesktop.DAL
{
    public class AccesoDatos
    {
        private readonly string _connectionString;
        private SqlConnection Cn;
        private SqlDataReader sdr;
        private SqlCommand Cmd;

        // Default Constructor
        public AccesoDatos(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public ArrayList arlListEquipo = new ArrayList();
        public ArrayList arlListLinea = new ArrayList();
        public ArrayList arlListMarca = new ArrayList();
        public ArrayList arlListOperarios = new ArrayList();

        public void IniciarBusqueda(string Tabla, string DatoBuscar, string Condicion)
        {
            try
            {
                Cn = new SqlConnection(_connectionString);
                Cmd = new SqlCommand("spr_CBuscarRegistro", Cn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                Cmd.Parameters.AddWithValue("p_DATOBUSCAR", DatoBuscar);
                Cmd.Parameters.AddWithValue("p_CONDICION", Condicion);
                Cn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader BuscarRegistro(string Tabla, string DatoBuscar, string Condicion)
        {
            IniciarBusqueda(Tabla, DatoBuscar, Condicion);
            sdr = Cmd.ExecuteReader();
            return sdr;
        }

        public int ValidarTablaVacia(string Tabla)
        {
            try
            {
                using (Cn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new SqlCommand("spr_CValidarExistenciaDatos", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteScalar();
                    int Resultado = Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value);
                    LiberarRecursos();
                    return Resultado;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArrayList CargarListas(string Tabla)
        {
            try
            {
                ArrayList arlLista = new ArrayList();
                using (Cn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
                {
                    Cmd = new SqlCommand("spr_CCargarCombosListas", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cn.Open();
                    using (sdr = Cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (sdr.Read())
                        {
                            arlLista.Add(new CargaCombosListas(sdr.GetValue(0).ToString(), sdr.GetValue(0).ToString() + " " + sdr.GetValue(1).ToString()));
                        }
                    sdr.Close();
                    LiberarRecursos();
                }
                return arlLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ArrayList CargarListas(string Tabla, string Condicion)
        {
            try
            {
                ArrayList arlListado = new ArrayList();
                using (Cn = new SqlConnection(_connectionString))
                {
                    Cmd = new SqlCommand("spr_CCargarListado", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cmd.Parameters.AddWithValue("p_CONDICION", Condicion);
                    Cn.Open();
                    using (sdr = Cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (sdr.Read())
                        {
                            arlListado.Add(new CargaCombosListas(sdr.GetValue(0).ToString(), sdr.GetValue(1).ToString()));
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

        public void ControlEquipos()
        {
            try
            {
                arlListEquipo = new ArrayList();
                arlListLinea = new ArrayList();
                arlListMarca = new ArrayList();
                using (Cn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Conexion"]))
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

        public void ControlProgramacion(string Tabla)
        {
            try
            {
                arlListEquipo = new ArrayList();
                arlListOperarios = new ArrayList();
                using (Cn = new SqlConnection(_connectionString))
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

        public Operario ObtenerOperario(string DatoBuscar, string Clave)
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
                    sdr.Close();
                    LiberarRecursos();
                    return operario;
                }
                else
                {
                    sdr.Close();
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GuardarOperario(Operario Operario, string Accion, double usuarioConectado)
        {
            try
            {
                using (Cn = new SqlConnection(_connectionString))
                {
                    Cmd = new SqlCommand("spr_IUOperarios", Cn);
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
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value);
                    LiberarRecursos();
                    return Resultado;
                }
            }
            catch (Exception e)
            {
                throw e;

            }

        }

        public bool GuardarCambioClave(string NuevaClave, double usuarioConectado)
        {
            try
            {
                using (Cn = new SqlConnection(_connectionString))
                {
                    Cmd = new SqlCommand("spr_UCambioClave", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_DOCUMENTO", usuarioConectado);
                    Cmd.Parameters.AddWithValue("p_CLAVE", NuevaClave);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    if (Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value) == 0)
                    {
                        LiberarRecursos();
                        return true;
                    }
                    LiberarRecursos();
                    return false;
                }
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
         */

        /*
		=======================================================================================================================================================
        Inicio Operaciones sobre estructura ListaValores
        =======================================================================================================================================================
		*/
        public ListaValores ObtenerListaValores(string DatoBuscar)
        {
            ListaValores listavalores = new ListaValores();
            try
            {

                sdr = (BuscarRegistro("LISTAVALORES", DatoBuscar, ""));
                if (sdr.Read())
                {
                    listavalores.codigo = Convert.ToInt32(sdr["CODIGO"].ToString());
                    listavalores.nombre = sdr["NOMBRE"].ToString();
                    listavalores.descripcion = sdr["DESCRIPCION"].ToString();
                    sdr.Close();
                    LiberarRecursos();
                    return listavalores;
                }
                else
                {
                    sdr.Close();
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GuardarListaValores(ListaValores listavalores, double usuarioConectado)
        {
            try
            {
                using (Cn = new SqlConnection(_connectionString))
                {
                    Cmd = new SqlCommand("spr_IUListaValores", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_CODIGO", listavalores.codigo);
                    Cmd.Parameters.AddWithValue("p_NOMBRE", listavalores.nombre);
                    Cmd.Parameters.AddWithValue("p_DESCRIPCION", listavalores.descripcion);
                    Cmd.Parameters.AddWithValue("p_TIPO", listavalores.tipo);
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value);
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
		*/

        /*
		=======================================================================================================================================================
        Inicio Operaciones sobre estructura Equipos
        =======================================================================================================================================================
		*/
        public Equipo ObtenerEquipo(string DatoBuscar)
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
                    if (sdr.GetBoolean(sdr.GetOrdinal("LUBRICACION"))) equipo.lubricacion = 1;
                    sdr.Close();
                    LiberarRecursos();
                    return equipo;
                }
                else
                {
                    sdr.Close();
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GuardarEquipo(Equipo equipo, double usuarioConectado)
        {
            try
            {
                using (Cn = new SqlConnection(_connectionString))
                {
                    Cmd = new SqlCommand("spr_IUEquipos", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_CODIGOEQUIPO", equipo.codigoequipo);
                    Cmd.Parameters.AddWithValue("p_NOMBREEQUIPO", equipo.nombreequipo);
                    Cmd.Parameters.AddWithValue("p_CODIGOMARCA", equipo.codigomarca);
                    Cmd.Parameters.AddWithValue("p_SERIE", equipo.serie);
                    Cmd.Parameters.AddWithValue("p_CODIGOLINEA", equipo.codigolinea);
                    Cmd.Parameters.AddWithValue("p_LUBRICACION", equipo.lubricacion);
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value);
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
       */

        /*
       =======================================================================================================================================================
       Inicio Operaciones sobre estructura Mantenimiento
       =======================================================================================================================================================
       */
        public Mantenimiento ObtenerMantenimiento(string DatoBuscar)
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
                    sdr.Close();
                    LiberarRecursos();
                    return mantenimiento;
                }
                else
                {
                    sdr.Close();
                    LiberarRecursos();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GuardarMantenimiento(Mantenimiento mantenimiento, string Accion, double usuarioConectado)
        {
            try
            {
                using (Cn = new SqlConnection(_connectionString))
                {
                    Cmd = new SqlCommand("spr_IUMantenimiento", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_ACCION", Accion);
                    Cmd.Parameters.AddWithValue("p_CODIGOEQUIPO", mantenimiento.codigoequipo);
                    Cmd.Parameters.AddWithValue("p_DOCUMENTO", mantenimiento.documento);
                    Cmd.Parameters.AddWithValue("p_FECHA", mantenimiento.fecha);
                    Cmd.Parameters.AddWithValue("p_OBSERVACIONES", mantenimiento.observaciones);
                    Cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value);
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


        public int EliminarRegistro(string DatoEliminar, string Tabla)
        {
            try
            {
                using (Cn = new SqlConnection(_connectionString))
                {
                    Cmd = new SqlCommand("spr_DRegistro", Cn);
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("p_TABLA", Tabla);
                    Cmd.Parameters.AddWithValue("p_CONDICION", DatoEliminar);
                    Cmd.Parameters.AddWithValue("p_RESULTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Cn.Open();
                    Cmd.ExecuteNonQuery();
                    int Resultado = Convert.ToInt32(Cmd.Parameters["p_RESULTADO"].Value);
                    LiberarRecursos();
                    return Resultado;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void LiberarRecursos()
        {
            Cmd.Dispose();
            if (Cn != null)
            {
                Cn.Close();
                Cn.Dispose();
            }
        }



    }
}
