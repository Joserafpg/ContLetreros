using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosbaseMaterias
    {
        public static int Agregar(DatosgetMaterias pget)
        {

            int retorno = 0;

            Conexion.opencon();

            SqlCommand Comando = new SqlCommand(string.Format("Insert into Materias (Materia, Maestro, Curso, Seccion, Area) values ('{0}','{1}','{2}','{3}','{4}')",
                    pget.Materia, pget.Maestro,  pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;

        }

        public static int Modificar(DatosgetMaterias pget)
        {
            int retorno = 0;
            Conexion.opencon();
            {
                SqlCommand comando = new SqlCommand(string.Format("UPDATE Materias SET Materia = '{0}', Maestro = '{1}', Curso = '{2}', Seccion = '{3}', Area = '{4}' WHERE Id_Materia = {5}",
                    pget.Materia, pget.Maestro, pget.Curso, pget.Seccion, pget.Area, pget.ID), Conexion.ObtenerConexion());

                retorno = comando.ExecuteNonQuery();
            }
            Conexion.cerrarcon();
            return retorno;
        }


        public static int Eliminar(Int64 pId)
        {
            int retorno = 0;
            Conexion.opencon();
            SqlCommand Comando = new SqlCommand(string.Format("DELETE FROM Materias WHERE Id_Materia = '{0}'", pId), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;
        }

        public static List<DatosgetMaterias> BuscarAlumnos(string pCurso, string pSeccion, string pArea, string nombre)
        {
            List<DatosgetMaterias> lista = new List<DatosgetMaterias>();
            Conexion.opencon();
            {

                SqlCommand comando = new SqlCommand(String.Format("SELECT Id_Materia, Materia, Maestro, Curso, Seccion, Area FROM Materias where Curso like '%{0}%' and Seccion like '%{1}%' and Area like '%{2}%' and Materia like '%{3}%' ", pCurso, pSeccion, pArea, nombre),
                    Conexion.ObtenerConexion());

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DatosgetMaterias pAlumnos = new DatosgetMaterias();
                    pAlumnos.ID = Convert.ToInt64(reader.GetValue(0));
                    pAlumnos.Materia = reader.GetString(1);
                    pAlumnos.Maestro = reader.GetString(2);
                    pAlumnos.Curso = reader.GetString(3);
                    pAlumnos.Seccion = reader.GetString(4);
                    pAlumnos.Area = reader.GetString(5);

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
