using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosgetMaestros
    {
        public Int64 Numero { get; set; }
        public string Nombre { get; set; }
        public string Curso { get; set; }
        public string Seccion { get; set; }
        public string Area { get; set; }

        public DatosgetMaestros() { }

        public DatosgetMaestros(Int64 pNumero, string pNombre, string pCurso, string pSeccion, string pArea)
        {
            this.Numero = pNumero;
            this.Nombre = pNombre;
            this.Curso = pCurso;
            this.Seccion = pSeccion;
            this.Area = pArea;
        }
    }
}
