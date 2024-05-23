using ContAlumnos.Clases.Estudiantes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos
{
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }

        private void Buscar()
        {
            DateTime fechapedido = txtfechapedido.Value;
            DateTime fechaentrega = txtfechaentrega.Value;

            long? id = null;

            // Intentar convertir el texto a un número, si no es válido, id permanecerá como null
            if (!string.IsNullOrEmpty(txtid.Text) && Int64.TryParse(txtid.Text, out long parsedId))
            {
                id = parsedId;
            }

            // Llamar al método BuscarAlumnos con el id (si existe)
            dataGridView1.DataSource = DatosbaseVentas.BuscarAlumnos(fechapedido, fechaentrega, id);
        }


        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            AgregarModificarPedidos formAgregar = new AgregarModificarPedidos();
            formAgregar.EditMode = false;
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                Buscar();
            }
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            Buscar();
        }

        private void txtfechapedido_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Obtén los datos de la fila seleccionada
                Int64 pedidoID = Convert.ToInt64(row.Cells[0].Value);
                int clienteID = Convert.ToInt32(row.Cells[1].Value);
                string nombreCliente = row.Cells[2].Value.ToString();
                string empleado = row.Cells[3].Value.ToString();
                decimal ancho = Convert.ToDecimal(row.Cells[4].Value);
                decimal largo = Convert.ToDecimal(row.Cells[5].Value);
                decimal precioMaterial = Convert.ToDecimal(row.Cells[6].Value);
                DateTime fechaPedido = Convert.ToDateTime(row.Cells[7].Value);
                DateTime fechaEntrega = Convert.ToDateTime(row.Cells[8].Value);
                decimal total = Convert.ToDecimal(row.Cells[9].Value);
                bool pagado = Convert.ToBoolean(row.Cells[10].Value);

                // Abre el formulario para editar el pedido
                AgregarModificarPedidos formEditar = new AgregarModificarPedidos();
                formEditar.EditMode = true; // Estás en modo editar
                formEditar.InitializeData(pedidoID, clienteID, nombreCliente, empleado, ancho, largo, precioMaterial, fechaPedido, fechaEntrega, total, pagado);
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    // Actualiza el DataGridView después de la edición
                    Buscar();
                }
            }
        }
    }
}
