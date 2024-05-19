using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Estudiantes
{
    public class DatosgetInventario
    {
        public Int64 Numero { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public string Categoria { get; set; }
        public Int64 Cantidad { get; set; }
        public Int64 Cantidad2 = 1;
        public string UnidadMedida { get; set; }
        public decimal CostoUnitario { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaCaducidad { get; set; }

        public DatosgetInventario() { }

        public DatosgetInventario(Int64 pNumero, string pNombre, string pDescripcion, string pCategoria, Int64 pCantidad, string pUnidadMedida, decimal pCostoUnitario, DateTime pFechaCompra, DateTime pFechaCaducidad
            )
        {
            this.Numero = pNumero;
            this.Nombre = pNombre;
            this.Descripción = pDescripcion;
            this.Categoria = pCategoria;
            this.Cantidad = pCantidad;
            this.UnidadMedida = pUnidadMedida;
            this.CostoUnitario = pCostoUnitario;
            this.FechaCompra = pFechaCompra;
            this.FechaCaducidad = pFechaCaducidad;
        }
    }
}
