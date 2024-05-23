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
    }
}
