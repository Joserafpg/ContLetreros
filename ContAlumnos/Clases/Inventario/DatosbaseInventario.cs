using ContAlumnos.Clases.Inventario;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosbaseInventario
    {
        public static int Agregar(DatosgetInventario pget)
        {

            int retorno = 0;

            Conexion.opencon();

            SqlCommand Comando = new SqlCommand(string.Format("Insert into Inventario (Nombre, Descripción, Categoría, Cantidad, UnidadMedida, CostoUnitario, FechaCompra, FechaCaducidad) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    pget.Nombre, pget.Descripción, pget.Categoria, pget.Cantidad, pget.UnidadMedida, pget.CostoUnitario, pget.FechaCompra.ToString("yyyy-MM-dd"), pget.FechaCaducidad.ToString("yyyy-MM-dd")), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;

        }

        public static int Agregar2(MateriaPrima pget)
        {

            int retorno = 0;

            Conexion.opencon();

            SqlCommand Comando = new SqlCommand(string.Format("Insert into Compras (FechaCompra, TotalCompra) values ('{0}','{1}')",
                    pget.FechaCompra.ToString("yyyy-MM-dd"), pget.TotalCompra), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;

        }

        public static int Modificar(DatosgetInventario pget)
        {
            int retorno = 0;
            Conexion.opencon();
            {
                SqlCommand comando = new SqlCommand(string.Format("update Inventario set Nombre = '{0}', Descripción = '{1}', Cantidad = '{2}', UnidadMedida = '{3}', CostoUnitario = '{4}', FechaCompra = '{5}', FechaCaducidad = '{6}', Categoría = '{7}' WHERE ID_MateriaPrima = {8}",
                    pget.Nombre, pget.Descripción, pget.Cantidad, pget.UnidadMedida, pget.CostoUnitario, pget.FechaCompra.ToString("yyyy-MM-dd"), pget.FechaCaducidad.ToString("yyyy-MM-dd"), pget.Categoria, pget.Numero), Conexion.ObtenerConexion());
                retorno = comando.ExecuteNonQuery();
            }
            Conexion.cerrarcon();
            return retorno;
        }

        public static int Eliminar(int pID)
        {
            int retorno = 0;
            Conexion.opencon();
            SqlCommand Comando = new SqlCommand(string.Format("DELETE FROM Inventario WHERE ID_MateriaPrima = {0}", pID), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;
        }

        public static List<DatosgetInventario> BuscarAlumnos(string pCurso, string pSeccion, string pArea, string nombre)
        {
            List<DatosgetInventario> lista = new List<DatosgetInventario>();
            Conexion.opencon();
            {

                SqlCommand comando = new SqlCommand(String.Format("SELECT ID_MateriaPrima, Nombre, Descripción, Categoría, Cantidad, UnidadMedida, CostoUnitario, FechaCompra, FechaCaducidad FROM Inventario where Categoría like '%{0}%' and UnidadMedida like '%{1}%' and Nombre like '%{2}%' and ID_MateriaPrima like '%{3}%' ", pCurso, pSeccion, pArea, nombre),
                    Conexion.ObtenerConexion());

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DatosgetInventario pAlumnos = new DatosgetInventario();
                    pAlumnos.Numero = Convert.ToInt64(reader.GetValue(0));
                    pAlumnos.Nombre = reader.GetString(1);
                    pAlumnos.Descripción = reader.GetString(2);
                    pAlumnos.Categoria = reader.GetString(3);
                    pAlumnos.Cantidad = Convert.ToInt64(reader.GetValue(4));
                    pAlumnos.UnidadMedida = reader.GetString(5);
                    pAlumnos.CostoUnitario = reader.GetDecimal(6);
                    pAlumnos.FechaCompra = reader.GetDateTime(7);
                    pAlumnos.FechaCaducidad = reader.GetDateTime(8);

                    lista.Add(pAlumnos);
                }
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
