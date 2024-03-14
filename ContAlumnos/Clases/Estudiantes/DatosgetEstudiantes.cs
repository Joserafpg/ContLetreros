using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosgetEstudiantes
    {
        public Int64 Numero { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public bool Discapacidad { get; set; }
        public string Curso { get; set; }
        public string Seccion { get; set; }
        public string Area { get; set; }

        public DatosgetEstudiantes() { }

        public DatosgetEstudiantes(Int64 pNumero, string pNombre, string pApellido, string pSexo, bool pDiscapacidad, string pCurso, string pSeccion, string pArea)
        {
            this.Numero = pNumero;
            this.Nombre = pNombre;
            this.Apellido = pApellido;
            this.Sexo = pSexo;
            this.Discapacidad = pDiscapacidad;
            this.Curso = pCurso;
            this.Seccion = pSeccion;
            this.Area = pArea;
        }
    }
}
