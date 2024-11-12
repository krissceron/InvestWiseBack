using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;
using System.Data.SqlClient;
using System.Data;

namespace InvestWiseProyecto.DataConnection
{
    public class PropuestaConnection
    {
        private string cadena = CadenaConexion.RetornaCadenaConexion();

        //INSERTAR USUARIO
        public Respuesta InsertarPropuesta(Propuesta propuesta)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_InsertarPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int)).Value = propuesta.idProducto;
                    command.Parameters.Add(new SqlParameter("@numInversionistasPropuesta", SqlDbType.Int)).Value = propuesta.numInversionistasPropuesta;
                    command.Parameters.Add(new SqlParameter("@presupuestoGastoPropuesta", SqlDbType.Float,5)).Value = propuesta.presupuestoGastoPropuesta;
                    command.Parameters.Add(new SqlParameter("@fechaInicioPropuesta", SqlDbType.VarChar,8)).Value = propuesta.fechaInicioPropuesta;
                   
                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;
                }
            }

            return respuesta;
        }

        //OBTENER USUARIOS
        public Respuesta ObtenerPropuestas()
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerPropuestas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión
                    connection.Open();

                    // Llenar DataTable con los datos obtenidos
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;

                    // Asignar el DataTable al atributo selectResultado de respuesta
                    //respuesta.selectResultado = dataTable;

                    // Convertir DataTable a lista de diccionarios y asignarlo a selectResultado
                    respuesta.selectResultado = ConvertDataTableToList(dataTable);
                }
            }

            return respuesta;
        }

        //OBTENER POR ID
        public Respuesta ObtenerPropuestaPorId(int idPropuesta)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerPropuestaPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idPropuesta", SqlDbType.Int)).Value = idPropuesta;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
                    connection.Open();

                    // Llenar DataTable con los datos obtenidos
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;

                    // Convertir DataTable a lista de diccionarios y asignarlo a selectResultado
                    respuesta.selectResultado = ConvertDataTableToList(dataTable);
                }
            }

            return respuesta;
        }



        // Método para actualizar usuario
        public Respuesta ActualizarPropuesta(PropuestaModificada propuestaModi)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ActualizarPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int)).Value = propuestaModi.idProducto;
                    command.Parameters.Add(new SqlParameter("@idEstadoPropuesta", SqlDbType.Int)).Value = propuestaModi.idEstadoPropuesta;
                    command.Parameters.Add(new SqlParameter("@numInversionistasPropuesta", SqlDbType.Int)).Value = propuestaModi.numInversionistasPropuesta;
                    command.Parameters.Add(new SqlParameter("@presupuestoGastoPropuesta", SqlDbType.Float)).Value = propuestaModi.presupuestoGastoPropuesta;
                    command.Parameters.Add(new SqlParameter("@fechaFinPropuesta", SqlDbType.DateTime)).Value = propuestaModi.fechaFinPropuesta;
                    command.Parameters.Add(new SqlParameter("@estaAprobado", SqlDbType.Bit)).Value = propuestaModi.estaAprobado;
                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;
                }
            }

            return respuesta;
        }

        //eliminar usuario
        // Método para eliminar usuario
        public Respuesta EliminarPropuesta(int idPropuesta)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_EliminarPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idPropuesta", SqlDbType.Int)).Value = idPropuesta;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;
                }
            }

            return respuesta;
        }


        //Convertimos la tabla

        private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                Dictionary<string, object> rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    rowDict[column.ColumnName] = row[column] != DBNull.Value ? row[column] : null;
                }
                list.Add(rowDict);
            }

            return list;
        }



    }
}
