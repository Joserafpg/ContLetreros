﻿using System;
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
                SqlCommand comando = new SqlCommand(string.Format("update Materias set Materia = '{0}' WHERE Maestro = '{1}',  Curso = '{2}' AND Seccion = '{3}' AND Area = '{4}'",
                    pget.Materia, pget.Maestro, pget.Curso, pget.Seccion, pget.Area), Conexion.ObtenerConexion());
                retorno = comando.ExecuteNonQuery();
            }
            Conexion.cerrarcon();
            return retorno;
        }

        public static int Eliminar(string materia, string maestro, string curso, string seccion, string area)
        {
            int retorno = 0;
            Conexion.opencon();
            SqlCommand Comando = new SqlCommand(string.Format("DELETE FROM Materias WHERE Materia = '{0}' AND Maestro = '{1}' AND Curso = '{2}' AND Seccion = '{3}' AND Area = '{4}'", materia, maestro, curso, seccion, area), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;
        }

        public static List<DatosgetMaterias> BuscarAlumnos(string pCurso, string pSeccion, string pArea)
        {
            List<DatosgetMaterias> lista = new List<DatosgetMaterias>();
            Conexion.opencon();
            {

                SqlCommand comando = new SqlCommand(String.Format("SELECT Materia, Maestro, Curso, Seccion, Area FROM Materias where Curso like '%{0}%' and Seccion like '%{1}%' and Area like '%{2}%' ", pCurso, pSeccion, pArea),
                    Conexion.ObtenerConexion());

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DatosgetMaterias pAlumnos = new DatosgetMaterias();
                    pAlumnos.Materia = reader.GetString(0);
                    pAlumnos.Maestro = reader.GetString(1);
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
