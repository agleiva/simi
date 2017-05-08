/*=================================================================================================================
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
using System.Data;
using ControlMantenimiento.Model;

namespace ControlMantenimiento.Data.Oracle
{
    public class OracleAccesoDatos: IAccesoDatos
    {
        private OracleConnection _connection;  
        private OracleDataReader _dataReader;  // Cursor - Recordset de solo lectura
        private OracleCommand _cmd;     // Objeto de tipo Command para acceder a Procedimientos Almacenados
        private readonly string _connectionString;

        public OracleAccesoDatos(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public ArrayList ArlListEquipo = new ArrayList();
        public ArrayList ArlListLinea = new ArrayList();
        public ArrayList ArlListMarca = new ArrayList();
        public ArrayList ArlListOperarios = new ArrayList();

        public int ValidarTablaVacia(string tabla)
        {
            try
            {
                using (_connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_CValidarExistenciaDatos", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_TABLA", tabla);
                    _cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    _connection.Open();
                    _cmd.ExecuteNonQuery();
                    int resultado = (Convert.ToInt32(_cmd.Parameters["p_RESULTADO"].Value));                   
                    LiberarRecursos();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArrayList CargarListas(string tabla)
        {
            try
            {
                ArrayList arlLista = new ArrayList();
                using (_connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_CCargarCombosListas", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_TABLA", tabla);
                    _connection.Open();
                    using (_dataReader = _cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (_dataReader.Read())
                        {
                            arlLista.Add(new CargaCombosListas(_dataReader.GetValue(0).ToString(), _dataReader.GetValue(0).ToString() + " " + _dataReader.GetValue(1).ToString()));
                        }
                    _dataReader.Close();
                    LiberarRecursos();
                }
                return arlLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ArrayList CargarListas(string tabla, string condicion)
        {
            try
            {
                ArrayList arlListado = new ArrayList();
                using ( _connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_CCargarListado", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_TABLA", tabla);
                    _cmd.Parameters.AddWithValue("p_CONDICION", condicion);
                    _connection.Open();
                    using (_dataReader = _cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    while (_dataReader.Read())
                    {
                        arlListado.Add(new CargaCombosListas(_dataReader.GetValue(0).ToString(),  _dataReader.GetValue(1).ToString()));
                    }
                    _dataReader.Close();
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
                ArlListEquipo = new ArrayList();
                ArlListLinea = new ArrayList();
                ArlListMarca = new ArrayList();
                using (_connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_CCargarCombosListas", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_TABLA", "CONTROLEQUIPOS");
                    _connection.Open();
                    using (_dataReader = _cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (_dataReader.Read())
                        {
                            if (_dataReader.GetValue(2).ToString().Equals("EQUIPOS"))
                            {
                                ArlListEquipo.Add(new CargaCombosListas(_dataReader.GetValue(0).ToString(), _dataReader.GetValue(0).ToString() + " " + _dataReader.GetValue(1).ToString()));
                            }
                            else if (_dataReader.GetValue(2).ToString().Equals("LINEAS"))
                            {
                                ArlListLinea.Add(new CargaCombosListas(_dataReader.GetValue(0).ToString(), _dataReader.GetValue(0).ToString() + " " + _dataReader.GetValue(1).ToString()));
                            }
                            else
                            {
                                ArlListMarca.Add(new CargaCombosListas(_dataReader.GetValue(0).ToString(), _dataReader.GetValue(0).ToString() + " " + _dataReader.GetValue(1).ToString()));
                            }
                        }
                    _dataReader.Close();
                    LiberarRecursos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ControlProgramacion(string tabla)
        {
            try
            {
                ArlListEquipo = new ArrayList();
                ArlListOperarios = new ArrayList();
                using (_connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_CCargarCombosListas", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_TABLA", tabla);
                    _connection.Open();
                    using (_dataReader = _cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (_dataReader.Read())
                        {
                            if (_dataReader.GetValue(2).ToString().Equals("EQUIPOS"))
                            {
                                ArlListEquipo.Add(new CargaCombosListas(_dataReader.GetValue(0).ToString(), _dataReader.GetValue(0).ToString() + " " + _dataReader.GetValue(1).ToString()));
                            }
                            else if (_dataReader.GetValue(2).ToString().Equals("OPERARIOS"))
                            {
                                ArlListOperarios.Add(new CargaCombosListas(_dataReader.GetValue(0).ToString(), _dataReader.GetValue(0).ToString() + " " + _dataReader.GetValue(1).ToString()));
                            }
                        }
                    _dataReader.Close();
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

        public Operario ObtenerOperario(string datoBuscar, string clave)
        {
            Operario operario = new Operario();
            try
            {

                _dataReader = (BuscarRegistro("OPERARIOS", datoBuscar, clave));
                if (_dataReader.Read())
                {
                    operario.documento = Convert.ToDouble(_dataReader["DOCUMENTO"].ToString());
                    operario.nombres = _dataReader["NOMBRES"].ToString();
                    operario.apellidos = _dataReader["APELLIDOS"].ToString();
                    operario.correo = _dataReader["CORREO"].ToString();
                    operario.telefono = Convert.ToDouble(_dataReader["TELEFONO"].ToString());
                    operario.clave = _dataReader["CLAVE"].ToString();
                    operario.perfil = Convert.ToInt32(_dataReader["PERFIL"].ToString());
                    operario.foto = _dataReader["FOTO"].ToString();
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

        public int GuardarOperario(Operario operario, string accion, double usuarioConectado)
        {
            try
            {
                using ( _connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_IUOperarios", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_ACCION", accion);
                    _cmd.Parameters.AddWithValue("p_DOCUMENTO", operario.documento);
                    _cmd.Parameters.AddWithValue("p_NOMBRES", operario.nombres);
                    _cmd.Parameters.AddWithValue("p_APELLIDOS", operario.apellidos);
                    _cmd.Parameters.AddWithValue("p_CORREO", operario.correo);
                    _cmd.Parameters.AddWithValue("p_TELEFONO", operario.telefono);
                    _cmd.Parameters.AddWithValue("p_CLAVE", operario.clave);
                    _cmd.Parameters.AddWithValue("p_PERFIL", operario.perfil);
                    _cmd.Parameters.AddWithValue("p_FOTO", operario.foto);
                    _cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    _cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    _connection.Open();
                    _cmd.ExecuteNonQuery();
                    int resultado = (Convert.ToInt32(_cmd.Parameters["p_RESULTADO"].Value));                    
                    LiberarRecursos();
                    return resultado;
                }                
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool GuardarCambioClave(string nuevaClave, double usuarioConectado)
        {
            bool status = false;
            try
            {
                using (_connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_UCambioClave", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_DOCUMENTO", usuarioConectado);
                    _cmd.Parameters.AddWithValue("p_CLAVE", nuevaClave);
                    _cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    _connection.Open();
                    _cmd.ExecuteNonQuery();
                    if (Convert.ToInt32(_cmd.Parameters["p_RESULTADO"].Value) == 0)
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
        public ListaValores ObtenerListaValores(string datoBuscar)
        {
            ListaValores listavalores = new ListaValores();
            try
            {

                _dataReader = (BuscarRegistro("LISTAVALORES", datoBuscar, "CODIGO"));
                if (_dataReader.Read())
                {
                    listavalores.codigo = Convert.ToInt32(_dataReader["CODIGO"].ToString());
                    listavalores.nombre = _dataReader["NOMBRE"].ToString();
                    listavalores.descripcion = _dataReader["DESCRIPCION"].ToString();
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

        public int GuardarListaValores(ListaValores listavalores, double usuarioConectado)
        {
            try
            {
                using ( _connection = new OracleConnection(_connectionString))
               
                {
                    _cmd = new OracleCommand("spr_IUListaValores", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_CODIGO", listavalores.codigo);
                    _cmd.Parameters.AddWithValue("p_NOMBRE", listavalores.nombre);
                    _cmd.Parameters.AddWithValue("p_DESCRIPCION", listavalores.descripcion);
                    _cmd.Parameters.AddWithValue("p_TIPO", listavalores.tipo);
                    _cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    _cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    _connection.Open();
                    _cmd.ExecuteNonQuery();
                    int resultado = (Convert.ToInt32(_cmd.Parameters["p_RESULTADO"].Value));                    
                    LiberarRecursos();
                    return resultado;
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
        public Equipo ObtenerEquipo(string datoBuscar)
        {
            Equipo equipo = new Equipo();
            try
            {

                _dataReader = (BuscarRegistro("EQUIPOS", datoBuscar, "Codigo"));
                if (_dataReader.Read())
                {
                    equipo.codigoequipo = Convert.ToInt32(_dataReader["CODIGOEQUIPO"].ToString());
                    equipo.nombreequipo = _dataReader["NOMBREEQUIPO"].ToString();
                    equipo.codigomarca = Convert.ToInt32(_dataReader["CODIGOMARCA"].ToString());
                    equipo.serie = _dataReader["SERIE"].ToString();
                    equipo.codigolinea = Convert.ToInt32(_dataReader["CODIGOLINEA"].ToString());
                    equipo.lubricacion = Convert.ToInt32(_dataReader["LUBRICACION"].ToString());
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

        public int GuardarEquipo(Equipo equipo, double usuarioConectado)
        {
            try
            {
                using (_connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_IUEquipos", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_CODIGOEQUIPO", equipo.codigoequipo);
                    _cmd.Parameters.AddWithValue("p_NOMBREEQUIPO", equipo.nombreequipo);
                    _cmd.Parameters.AddWithValue("p_CODIGOMARCA", equipo.codigomarca);
                    _cmd.Parameters.AddWithValue("p_SERIE", equipo.serie);
                    _cmd.Parameters.AddWithValue("p_CODIGOLINEA", equipo.codigolinea);
                    _cmd.Parameters.AddWithValue("p_LUBRICACION", equipo.lubricacion);
                    _cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    _cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    _connection.Open();
                    _cmd.ExecuteNonQuery();
                    int resultado = (Convert.ToInt32(_cmd.Parameters["p_RESULTADO"].Value));                    
                    LiberarRecursos();
                    return resultado;
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
        public Mantenimiento ObtenerMantenimiento(string datoBuscar)
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            try
            {

                _dataReader = (BuscarRegistro("MANTENIMIENTO", datoBuscar, ""));
                if (_dataReader.Read())
                {
                    mantenimiento.codigoequipo = Convert.ToInt32(_dataReader["CODIGOEQUIPO"].ToString());
                    mantenimiento.documento = Convert.ToDouble(_dataReader["DOCUMENTO"].ToString());
                    mantenimiento.fecha = Convert.ToDateTime(_dataReader["FECHA"].ToString());
                    mantenimiento.observaciones = _dataReader["OBSERVACIONES"].ToString();
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

        public int GuardarMantenimiento(Mantenimiento mantenimiento, string accion, double usuarioConectado)
        {
            try
            {
                using ( _connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_IUMantenimiento", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_ACCION", accion);
                    _cmd.Parameters.AddWithValue("p_CODIGOEQUIPO", mantenimiento.codigoequipo);
                    _cmd.Parameters.AddWithValue("p_DOCUMENTO", mantenimiento.documento);
                    _cmd.Parameters.AddWithValue("p_FECHA", mantenimiento.fecha);
                    _cmd.Parameters.AddWithValue("p_OBSERVACIONES", mantenimiento.observaciones);
                    _cmd.Parameters.AddWithValue("p_USUARIOCONECTADO", usuarioConectado);
                    _cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    _connection.Open();
                    _cmd.ExecuteNonQuery();
                    int resultado = (Convert.ToInt32(_cmd.Parameters["p_RESULTADO"].Value));
                    LiberarRecursos();
                    return resultado;
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
        
        public int EliminarRegistro(string datoEliminar, string tabla)
        {
            bool status = false;
            try
            {
                using ( _connection = new OracleConnection(_connectionString))
                {
                    _cmd = new OracleCommand("spr_DRegistro", _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("p_TABLA", tabla);
                    _cmd.Parameters.AddWithValue("p_CONDICION", datoEliminar);
                    _cmd.Parameters.AddWithValue("p_RESULTADO", OracleType.Int32).Direction = ParameterDirection.Output;
                    _connection.Open();
                    _cmd.ExecuteNonQuery();
                    int resultado = (Convert.ToInt32(_cmd.Parameters["p_RESULTADO"].Value));
                    LiberarRecursos();
                    return resultado;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private OracleDataReader BuscarRegistro(string tabla, string datoBuscar, string condicion)
        {
            IniciarBusqueda(tabla, datoBuscar, condicion);
            _dataReader = _cmd.ExecuteReader();
            return _dataReader;
        }

        private void IniciarBusqueda(string tabla, string datoBuscar, string condicion)
        {
            try
            {
                _connection = new OracleConnection(_connectionString);
                _cmd = new OracleCommand("spr_CBuscarRegistro", _connection);
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("p_TABLA", tabla);
                _cmd.Parameters.AddWithValue("p_DATOBUSCAR", datoBuscar);
                _cmd.Parameters.AddWithValue("p_CONDICION", condicion);
                _cmd.Parameters.Add("Out_Data", OracleType.Cursor).Direction = ParameterDirection.Output;
                _connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LiberarRecursos()
        {
            if (!_dataReader.IsClosed) _dataReader.Close();
            _cmd.Dispose();
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

    }
}

