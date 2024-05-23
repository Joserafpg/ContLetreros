using ContAlumnos.Clases.Ventas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosbaseVentas
    {
        public static int Agregar(DatosgetVentas pget)
        {

            int retorno = 0;

            Conexion.opencon();

            SqlCommand Comando = new SqlCommand(string.Format("Insert into Pedidos (ClienteID, NombreCliente, Empleado, Ancho, Largo, Precio_material, FechaPedido, FechaEntrega, Total, Pagado) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                    pget.ClienteID, pget.NombreCliente, pget.Empleado, pget.Ancho, pget.Largo, pget.Precio_material, pget.FechaPedido.ToString("yyyy-MM-dd"),  pget.FechaEntrega.ToString("yyyy-MM-dd"), pget.Total, pget.Pagado), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;

        }

        public static int Modificar(DatosgetVentas pget)
        {
            int retorno = 0;
            Conexion.opencon();
            {
                SqlCommand comando = new SqlCommand(string.Format("update Pedidos set ClienteID = '{0}', Empleado = '{1}', FechaPedido = '{2}', FechaEntrega = '{3}', Total = '{4}', Pagado = '{5}'' WHERE PedidoID = {6}",
                    pget.ClienteID, pget.NombreCliente, pget.Empleado, pget.FechaPedido.ToString("yyyy-MM-dd"), pget.FechaEntrega.ToString("yyyy-MM-dd"), pget.Total, pget.Pagado, pget.PedidoID), Conexion.ObtenerConexion());
                retorno = comando.ExecuteNonQuery();
            }
            Conexion.cerrarcon();
            return retorno;
        }

        public static int Eliminar(int pID)
        {
            int retorno = 0;
            Conexion.opencon();
            SqlCommand Comando = new SqlCommand(string.Format("DELETE FROM Pedidos WHERE PedidoID = {0} ", pID), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;
        }

        public static List<DatosgetVentas> BuscarAlumnos(DateTime fechapedido, DateTime fechaentrega, long? pedidoID = null)
        {
            List<DatosgetVentas> lista = new List<DatosgetVentas>();
            Conexion.opencon();
            {
                // Construcción de la consulta para permitir la búsqueda por ID y/o por rango de fechas
                string query =
                    "SELECT PedidoID, ClienteID, NombreCliente, Empleado, Ancho, Largo, Precio_material,FechaPedido, FechaEntrega, Total, Pagado " +
                    "FROM Pedidos " +
                    "WHERE (@PedidoID IS NOT NULL AND PedidoID = @PedidoID) " +
                    "OR (FechaEntrega BETWEEN @FechaInicio AND @FechaFin)";

                SqlCommand comando = new SqlCommand(query, Conexion.ObtenerConexion());
                comando.Parameters.AddWithValue("@FechaInicio", fechapedido);
                comando.Parameters.AddWithValue("@FechaFin", fechaentrega);

                // Añadir el parámetro PedidoID, aunque sea nulo
                if (pedidoID.HasValue)
                {
                    comando.Parameters.AddWithValue("@PedidoID", pedidoID.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@PedidoID", DBNull.Value);
                }

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DatosgetVentas pAlumnos = new DatosgetVentas();
                    pAlumnos.PedidoID = Convert.ToInt64(reader.GetValue(0));
                    pAlumnos.ClienteID = Convert.ToInt64(reader.GetValue(1));
                    pAlumnos.NombreCliente = reader.GetString(2);
                    pAlumnos.Empleado = reader.GetString(3);
                    pAlumnos.FechaPedido = reader.GetDateTime(4);
                    pAlumnos.FechaEntrega = reader.GetDateTime(5);
                    pAlumnos.Total = Convert.ToDouble(reader.GetValue(6));
                    pAlumnos.Pagado = reader.GetBoolean(7);

                    lista.Add(pAlumnos);
                }
                reader.Close(); // Asegúrate de cerrar el reader
                Conexion.cerrarcon();
                return lista;
            }
        }




        /*public static DatosgetEstudiantes ObtenerAlumnos(string pId)
        {
            Conexion.opencon();
            {
                DatosgetEstudiantes pAlumno = new DatosgetEstudiantes();
                SqlCommand comando = new SqlCommand(string.Format(
                   "select codigo, Nombre, Cedula, Telefono, Direccion, Fecha_nacimiento From Alumnos where Codigo = {0}", pId), Conexion.ObtenerConexion());
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    pAlumno.Codigo = Convert.ToInt64(reader.GetValue(0));
                    pAlumno.Nombre = reader.GetString(1);
                    pAlumno.Precio_Compra = reader.GetDecimal(2);
                    pAlumno.Precio = reader.GetDecimal(3);
                    pAlumno.Cantidad = reader.GetInt64(4);
                    pAlumno.Departamento = reader.GetString(5);
                    pAlumno.Fecha_Ingreso = Convert.ToDateTime(reader.GetValue(6));
                }
                Conexion.cerrarcon();
                return pAlumno;
            }
        }*/
    }
}
