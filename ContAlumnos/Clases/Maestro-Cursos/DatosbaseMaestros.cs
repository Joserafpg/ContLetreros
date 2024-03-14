using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosbaseMaestros
    {
        public static int Agregar(DatosgetMaestros pget)
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

        public static int Agregar2(DatosgetMaestros pget)
        {
            int retorno = 0;

            // Verificar si el curso ya existe
            if (CursoExiste(pget.Curso, pget.Seccion, pget.Area))
            {
                // Si el curso ya existe, modificar en lugar de agregar
                retorno = Modificar(pget);
            }
            else
            {
                Conexion.opencon();

                SqlCommand Comando = new SqlCommand(string.Format("Insert into Curso (Maestro_Titular, Curso, Seccion, Area) values ('{0}','{1}','{2}','{3}')",
                        pget.Nombre, pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());

                retorno = Comando.ExecuteNonQuery();

                Conexion.cerrarcon();
            }

            return retorno;
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

        public static int Modificar2(DatosgetMaestros pget)
        {
            int retorno = 0;
            Conexion.opencon();
            {
                SqlCommand comando = new SqlCommand(string.Format("update Maestros set Nombre = '{0}' WHERE Id_Maestro = {1} AND Curso = '{2}' AND Seccion = '{3}' AND Area = '{4}'",
                   pget.Nombre, pget.Numero, pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());
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
        }

        public static int Eliminar2(string curso, string seccion, string area)
        {
            int retorno = 0;
            Conexion.opencon();
            SqlCommand Comando = new SqlCommand(string.Format("update Curso set Maestro_Titular = ' ' WHERE Curso = '{0}' AND Seccion = '{1}' AND Area = '{2}'", curso, seccion, area), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;
        }

        public static List<DatosgetMaestros> BuscarAlumnos(string pCurso, string pSeccion, string pArea)
        {
            List<DatosgetMaestros> lista = new List<DatosgetMaestros>();
            Conexion.opencon();
            {

                SqlCommand comando = new SqlCommand(String.Format("SELECT Id_Maestro, Nombre, Curso, Seccion, Area FROM Maestros where Curso like '%{0}%' and Seccion like '%{1}%' and Area like '%{2}%' ", pCurso, pSeccion, pArea),
                    Conexion.ObtenerConexion());

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DatosgetMaestros pAlumnos = new DatosgetMaestros();
                    pAlumnos.Numero = Convert.ToInt64(reader.GetValue(0));
                    pAlumnos.Nombre = reader.GetString(1);
                    pAlumnos.Curso = reader.GetString(2);
                    pAlumnos.Seccion = reader.GetString(3);
                    pAlumnos.Area = reader.GetString(4);

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
