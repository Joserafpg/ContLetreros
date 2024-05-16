using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosgetClientes
    {
        public Int64 Numero { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Sexo { get; set; }
        public DateTime Fecha_Ingreso { get; set; }

        public DatosgetClientes() { }

        public DatosgetClientes(Int64 pNumero, string pNombre, string pApellido, string pCedula, string pSexo, DateTime pFecha)
        {
            this.Numero = pNumero;
            this.Nombre = pNombre;
            this.Apellido = pApellido;
            this.Cedula = pCedula;
            this.Sexo = pSexo;
            this.Fecha_Ingreso = pFecha;
        }
    }
}
