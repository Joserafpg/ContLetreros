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

        void Buscar()
        {
            dataGridView1.DataSource = DatosbaseVentas.BuscarAlumnos();
        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            AgregarModificarMateriaPrima formAgregar = new AgregarModificarMateriaPrima();
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
