using System;
using ControlMantenimiento.Data;
using ControlMantenimiento.Data.MySql;
using ControlMantenimiento.Data.Oracle;

namespace ControlMantenimiento.Business
{
    public static class AccesoDatosFactory
    {
        public static IAccesoDatos GetAccesoDatos(string connectionString, string providerName)
        {
            switch (providerName)
            {
                case "System.Data.SqlClient": return new AccesoDatos(connectionString);
                case "MySql.Data.MySqlClient": return new MySqlAccesoDatos(connectionString);
                case "System.Data.OracleClient": return new OracleAccesoDatos(connectionString);
                default: throw new NotImplementedException($"El proveedor de acceso a datos {providerName} no está implementado.");
            }
            
        }
            
    }
}