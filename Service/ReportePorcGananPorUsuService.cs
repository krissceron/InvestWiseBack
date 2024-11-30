using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model.Reportes;
using System.Data.SqlClient;

namespace InvestWiseProyecto.Service
{
    public class ReportePorcGananPorUsuService
    {
        private string _cadenaConexion;

        public ReportePorcGananPorUsuService()
        {
            _cadenaConexion = CadenaConexion.RetornaCadenaConexion();
        }
        public List<object> GenerarReportePorUsuario(int idUsuario)
        {
            // Cargar datos desde la base de datos
            var usuarios = ObtenerUsuarios();
            var productos = ObtenerProductos();
            var propuestas = ObtenerPropuestas();
            var usuariosPropuestas = ObtenerUsuariosPropuestas();

            // Filtrar el usuario por ID
            var usuarioFiltrado = usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuarioFiltrado == null)
            {
                return new List<object>(); // Retorna vacío si no hay usuario
            }

            // Realizar el reporte con LINQ
            var reporte = (from up in usuariosPropuestas
                           join p in propuestas on up.IdPropuesta equals p.IdPropuesta
                           join prod in productos on p.IdProducto equals prod.IdProducto
                           where up.IdUsuario == idUsuario && !p.EstaAprobado // Condiciones
                           let ingresoPorUsuario = (p.PrecioVentaPropuesta - p.ValorTotalPropuesta) /
                                                   usuariosPropuestas.Count(x => x.IdPropuesta == p.IdPropuesta)
                           let porcentajeGananciaFinal = (ingresoPorUsuario / up.MontoInversion) * 100
                           let fechaInicio = ConvertirFechaYYYYMMDD(p.FechaInicioPropuesta)
                           let fechaAceptacion = ConvertirFechaYYYYMMDD(up.FechaAceptacion)
                           let rotacionDias = (fechaInicio.HasValue && fechaAceptacion.HasValue) ?
                                              (int?)(fechaAceptacion.Value - fechaInicio.Value).TotalDays : null

                           let rentabilidadPorDia = rotacionDias.HasValue && rotacionDias > 0 ?
                                                    porcentajeGananciaFinal / rotacionDias.Value : 0
                           let analisisRentabilidad = rentabilidadPorDia!=0
                               ? (rentabilidadPorDia > 5 ? "Alta rentabilidad, buena inversión" :
                                  porcentajeGananciaFinal == 100 ? "Recuperaste lo invertido. Considera nuevas oportunidades." :
                                  "Baja rentabilidad, reconsiderar inversión")
                               : "Datos insuficientes para análisis"
                           select new
                           {
                               Producto = prod.NombreProducto,
                               InversionTotalPropuesta = p.ValorTotalPropuesta,
                               PrecioDeVenta = p.PrecioVentaPropuesta,
                               Participantes = p.NumInversionistasPropuesta,
                               InversionIndividual = up.MontoInversion,
                               IngresoPropuestaPorUsuario = ingresoPorUsuario,
                               PorcentajeGananciaFinal = porcentajeGananciaFinal,
                               ObjPorcGananciaIndiv = usuarioFiltrado.ObjPorcPropUsuario,
                               RotacionDias = rotacionDias,
                               RentabilidadPorDia = rentabilidadPorDia,
                               AnalisisRentabilidad = analisisRentabilidad
                           }).ToList<object>();

            return reporte;
        }


        private List<UsuarioRPorcGanan> ObtenerUsuarios()
        {
            var usuarios = new List<UsuarioRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM Usuario";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new UsuarioRPorcGanan
                            {
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                NombreApellido = reader["nombreApellido"].ToString(),
                                CorreoUsuario = reader["correoUsuario"].ToString(),
                                GeneroUsuario = reader["generoUsuario"].ToString(),
                                FechaNacimientoUsuario = Convert.ToDateTime(reader["fechaNacimientoUsuario"])
                                                  .ToString("yyyyMMdd"),
                                ObjPorcPropUsuario = Convert.ToSingle(reader["objPorcPropUsuario"])
                            });
                        }
                    }
                }
            }
            return usuarios;
        }

        private List<ProductoRPorcGanan> ObtenerProductos()
        {
            var productos = new List<ProductoRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM Producto";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new ProductoRPorcGanan
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                NombreProducto = reader["nombreProducto"].ToString()
                            });
                        }
                    }
                }
            }
            return productos;
        }

        private List<PropuestaRPorcGanan> ObtenerPropuestas()
        {
            var propuestas = new List<PropuestaRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM Propuesta";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            propuestas.Add(new PropuestaRPorcGanan
                            {
                                IdPropuesta = Convert.ToInt32(reader["idPropuesta"]),
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                ValorTotalPropuesta = Convert.ToSingle(reader["valorTotalPropuesta"]),
                                PrecioVentaPropuesta = Convert.ToSingle(reader["precioVentaPropuesta"]),
                                EstaAprobado = Convert.ToBoolean(reader["estaAprobado"]),
                                NumInversionistasPropuesta = Convert.ToInt32(reader["numInversionistasPropuesta"]),
                                FechaInicioPropuesta = Convert.ToDateTime(reader["fechaInicioPropuesta"])
                                                  .ToString("yyyyMMdd"),
                            });
                        }
                    }
                }
            }
            return propuestas;
        }

        private List<UsuPropRPorcGanan> ObtenerUsuariosPropuestas()
        {
            var usuariosPropuestas = new List<UsuPropRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM UsuarioPropuesta";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuariosPropuestas.Add(new UsuPropRPorcGanan
                            {
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                IdPropuesta = Convert.ToInt32(reader["idPropuesta"]),
                                MontoInversion = Convert.ToSingle(reader["montoInversion"]),
                                FechaAceptacion = Convert.ToDateTime(reader["fechaAceptacion"])
                                                  .ToString("yyyyMMdd"),
                            });
                        }
                    }
                }
            }
            return usuariosPropuestas;
        }

        private DateTime? ConvertirFechaYYYYMMDD(string fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
            {
                return null; // Retorna null si la cadena es nula o vacía
            }

            if (DateTime.TryParseExact(fecha, "yyyyMMdd",
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None,
                                       out DateTime fechaConvertida))
            {
                return fechaConvertida;
            }
            else
            {
                return null; // Retorna null si no es posible convertir la fecha
            }
        }


        //private int CalcularEdadDesdeFormatoYYYYMMDD(string fechaNacimiento)
        //{
        //    if (DateTime.TryParseExact(fechaNacimiento, "yyyyMMdd",
        //                               System.Globalization.CultureInfo.InvariantCulture,
        //                               System.Globalization.DateTimeStyles.None,
        //                               out DateTime fechaNacimientoDate))
        //    {
        //        var today = DateTime.Today;
        //        int edad = today.Year - fechaNacimientoDate.Year;

        //        // Ajustar la edad si el cumpleaños aún no ha ocurrido este año
        //        if (fechaNacimientoDate > today.AddYears(-edad))
        //        {
        //            edad--;
        //        }

        //        return edad;
        //    }
        //    else
        //    {
        //        throw new FormatException("La fecha de nacimiento no tiene el formato esperado (yyyyMMdd).");
        //    }
        //}

    }
}