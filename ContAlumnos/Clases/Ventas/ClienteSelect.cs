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

namespace ContAlumnos.Clases.Ventas
{
    public partial class ClienteSelect : Form
    {
        public ClienteSelect()
        {
            InitializeComponent();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            AgregarModificarPedidos frm = Owner as AgregarModificarPedidos;

            string codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            // El registro no existe, agrega una nueva fila con los valores
            frm.ccodigo = (codigo);
            frm.cnombre = (nombre);

            this.Close();
        }

        private void ClienteSelect_Load(object sender, EventArgs e)
        {
            string query = "SELECT ID_Cliente, Nombre FROM Clientes ";

            Conexion.opencon();
            SqlCommand cmd = new SqlCommand(query, Conexion.ObtenerConexion());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Conexion.cerrarcon();
        }
    }
}
