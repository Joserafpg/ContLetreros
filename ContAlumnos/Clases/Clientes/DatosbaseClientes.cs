using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosbaseClientes
    {
        /*public static int Agregar(DatosgetMaestros pget)
        {
            int retorno = 0;

            // Verificar si el maestro ya existe
            if (MaestroExiste(pget.Nombre, pget.Curso, pget.Seccion, pget.Area))
            {
                // Si el maestro ya existe, mostrar un mensaje y retornar sin agregar
                MessageBox.Show("El maestro ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retorno;
            }

            Conexion.opencon();

            SqlCommand Comando = new SqlCommand(string.Format("Insert into Maestros (Nombre, Curso, Seccion, Area) values ('{0}','{1}','{2}','{3}')",
                    pget.Nombre, pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();

            Conexion.cerrarcon();

            return retorno;
        }

        public static bool MaestroExiste(string nombre, string curso, string seccion, string area)
        {
            Conexion.opencon();

            SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Maestros WHERE Nombre = @Nombre AND Curso = @Curso AND Seccion = @Seccion AND Area = @Area", Conexion.ObtenerConexion());
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Curso", curso);
            comando.Parameters.AddWithValue("@Seccion", seccion);
            comando.Parameters.AddWithValue("@Area", area);

            int count = (int)comando.ExecuteScalar();

            Conexion.cerrarcon();

            return count > 0;
        }

        public static bool CursoExiste(string curso, string seccion, string area)
        {
            Conexion.opencon();

            SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Curso WHERE Curso = @Curso AND Seccion = @Seccion AND Area = @Area", Conexion.ObtenerConexion());
            comando.Parameters.AddWithValue("@Curso", curso);
            comando.Parameters.AddWithValue("@Seccion", seccion);
            comando.Parameters.AddWithValue("@Area", area);

            int count = (int)comando.ExecuteScalar();

            Conexion.cerrarcon();

            return count > 0;
        }

        public static int Modificar(DatosgetMaestros pget)
        {
            int retorno = 0;
            Conexion.opencon();
            {
                SqlCommand comando = new SqlCommand(string.Format("update Curso set Maestro_Titular = '{0}' WHERE Curso = '{1}' AND Seccion = '{2}' AND Area = '{3}'",
                   pget.Nombre,  pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());
                retorno = comando.ExecuteNonQuery();
            }
            Conexion.cerrarcon();
            return retorno;
        }

        public static int Eliminar(int pID)
        {
            int retorno = 0;
            Conexion.opencon();
            SqlCommand Comando = new SqlCommand(string.Format("DELETE FROM Maestros WHERE Id_Maestro = {0} ", pID), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;
        }*/

        public static List<DatosgetClientes> BuscarAlumnos(string cedula)
        {
            List<DatosgetClientes> lista = new List<DatosgetClientes>();
            Conexion.opencon();
            {

                SqlCommand comando = new SqlCommand(String.Format("SELECT ID_Cliente, Nombre, Apellido, Cedula, Sexo, Fecha_Ingreso FROM Clientes where Cedula like '%{0}%'", cedula),
                    Conexion.ObtenerConexion());

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DatosgetClientes pAlumnos = new DatosgetClientes();
                    pAlumnos.Numero = Convert.ToInt64(reader.GetValue(0));
                    pAlumnos.Nombre = reader.GetString(1);
                    pAlumnos.Apellido = reader.GetString(2);
                    pAlumnos.Cedula = reader.GetString(3);
                    pAlumnos.Sexo = reader.GetString(4);
                    pAlumnos.Fecha_Ingreso = reader.GetDateTime(5);

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
