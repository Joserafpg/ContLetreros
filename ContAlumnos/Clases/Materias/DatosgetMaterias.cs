using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosgetMaterias
    {
        public Int64 ID { get; set; }
        public string Materia { get; set; }
        public string Maestro { get; set; }
        public string Curso { get; set; }
        public string Seccion { get; set; }
        public string Area { get; set; }

        public DatosgetMaterias() { }

        public DatosgetMaterias(Int64 pid, string pMateria, string pMaestro, string pCurso, string pSeccion, string pArea)
        {
            this.ID = pid;
            this.Materia = pMateria;
            this.Maestro = pMaestro;
            this.Curso = pCurso;
            this.Seccion = pSeccion;
            this.Area = pArea;
        }
    }
}
