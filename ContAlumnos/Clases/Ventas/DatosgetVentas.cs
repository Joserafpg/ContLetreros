using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Ventas
{
    public class DatosgetVentas
    {
        public Int64 PedidoID { get; set; }
        public Int64 ClienteID { get; set; }
        public string NombreCliente { get; set; }
        public string Empleado { get; set; }
        public decimal Ancho { get; set; }
        public decimal Largo { get; set; }
        public decimal Precio_material { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public double Total { get; set; }
        public bool Pagado { get; set; }

    }
}
