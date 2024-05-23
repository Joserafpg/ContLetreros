using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Ventas
{
    public class DetallePedido
    {
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        public string Producto { get; set; }
        public string DescripciónProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}