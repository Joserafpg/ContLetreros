using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosbaseEstudiantes
    {
        public static int Agregar(DatosgetEstudiantes pget)
        {

            int retorno = 0;

            Conexion.opencon();

            SqlCommand Comando = new SqlCommand(string.Format("Insert into Estudiantes (Numero, Nombre, Apellido, Sexo, Discapacidad, Curso, Seccion, Area) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    pget.Numero, pget.Nombre, pget.Apellido, pget.Sexo, pget.Discapacidad, pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;

        }

        public static int Modificar(DatosgetEstudiantes pget)
        {
            int retorno = 0;
            Conexion.opencon();
            {
                SqlCommand comando = new SqlCommand(string.Format("update Estudiantes set Nombre = '{0}', Apellido = '{1}', Sexo = '{2}', Discapacidad = '{3}' WHERE Numero = {4} AND Curso = '{3}' AND Seccion = '{3}' AND Area = '{3}'",
                    pget.Nombre, pget.Apellido, pget.Sexo, pget.Discapacidad, pget.Numero, pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());
                retorno = comando.ExecuteNonQuery();
            }
            Conexion.cerrarcon();
            return retorno;
        }

        public static int Eliminar(int pID, string curso, string seccion, string area)
        {
            int retorno = 0;
            Conexion.opencon();
            SqlCommand Comando = new SqlCommand(string.Format("DELETE FROM Estudiantes WHERE Numero = {0} AND Curso = '{1}' AND Seccion = '{2}' AND Area = '{3}'", pID, curso, seccion, area), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;
        }

        public static List<DatosgetEstudiantes> BuscarAlumnos(string pCurso, string pSeccion, string pArea)
        {
            List<DatosgetEstudiantes> lista = new List<DatosgetEstudiantes>();
            Conexion.opencon();
            {

                SqlCommand comando = new SqlCommand(String.Format("SELECT Numero, Nombre, Apellido, Sexo, Discapacidad, Curso, Seccion, Area FROM Estudiantes where Curso like '%{0}%' and Seccion like '%{1}%' and Area like '%{2}%' ", pCurso, pSeccion, pArea),
                    Conexion.ObtenerConexion());

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DatosgetEstudiantes pAlumnos = new DatosgetEstudiantes();
                    pAlumnos.Numero = Convert.ToInt64(reader.GetValue(0));
                    pAlumnos.Nombre = reader.GetString(1);
                    pAlumnos.Apellido = reader.GetString(2);
                    pAlumnos.Sexo = reader.GetString(3);
                    pAlumnos.Discapacidad = reader.GetBoolean(4);
                    pAlumnos.Curso = reader.GetString(5);
                    pAlumnos.Seccion = reader.GetString(6);
                    pAlumnos.Area = reader.GetString(7);

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
