using ContAlumnos.Clases.Estudiantes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class Materias : Form
    {
        public Materias()
        {
            InitializeComponent();
        }

        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContAlumnos; Integrated Security=True");

        void CargarComboBox()
        {
            Conn.Open();
            string consulta = "SELECT DISTINCT Curso FROM Estudiantes";
            SqlCommand comando = new SqlCommand(consulta, Conn);
            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                txtcurso.Items.Add(lector.GetString(0));
            }

            Conn.Close();

            Conn.Open();
            string consulta2 = "SELECT DISTINCT Seccion FROM Estudiantes";
            SqlCommand comando2 = new SqlCommand(consulta2, Conn);
            SqlDataReader lector2 = comando2.ExecuteReader();

            while (lector2.Read())
            {
                txtseccion.Items.Add(lector2.GetString(0));
            }

            Conn.Close();

            Conn.Open();
            string consulta3 = "SELECT DISTINCT Area FROM Estudiantes";
            SqlCommand comando3 = new SqlCommand(consulta3, Conn);
            SqlDataReader lector3 = comando3.ExecuteReader();

            while (lector3.Read())
            {
                txtarea.Items.Add(lector3.GetString(0));
            }

            Conn.Close();
        }

        void Buscar()
        {
            dataGridView1.DataSource = DatosbaseMaterias.BuscarAlumnos(txtcurso.Text, txtseccion.Text, txtarea.Text);
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Estudiantes_Load(object sender, EventArgs e)
        {
            CargarComboBox();
            Buscar();
        }

        private void txtcurso_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtcurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Obtén los datos de la fila seleccionada
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                string materia = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                string maestro = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                string curso = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                string seccion = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
                string area = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();

                // Abre el formulario para editar el producto
                AgregarModificarMaterias formEditar = new AgregarModificarMaterias();
                formEditar.EditMode = true; // Estás en modo editar
                formEditar.InitializeData(materia, maestro, curso, seccion, area);
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    // Actualiza el DataGridView después de la edición
                    Buscar();
                }
            }
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debes seleccionar un estudiantes", "Seleccionar un estudiante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if(dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Esta seguro que desea eliminar el estudiante actual??", "Esta Seguro?!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    string materia = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                    string maestro = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString(); 
                    string curso = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                    string seccion = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
                    string area = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();

                    Int64 resultado = DatosbaseMaterias.Eliminar(materia, maestro, curso, seccion, area);
                    if (resultado > 0)
                    {
                        MessageBox.Show("Estudiante eliminado", "Estudiante Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Refresh();
                        Buscar();
                    }

                    else
                    {
                        MessageBox.Show("No se pudo eliminar al estudiante", "estudiante eliminado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                else
                    MessageBox.Show("Se cancelo la eliminacion", "Cancelado");
            }
            else
            {
                MessageBox.Show("No se pudo eliminar mas de un estudiante a la ves", "Seleccionar un estudiante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            AgregarModificarMaterias formAgregar = new AgregarModificarMaterias();
            formAgregar.EditMode = false;
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                Buscar();
            }
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcurso.Text) && !string.IsNullOrEmpty(txtseccion.Text) && !string.IsNullOrEmpty(txtarea.Text))
            {
                PlantillaReporte form = new PlantillaReporte();
                ReportDocument oRep = new ReportDocument();
                ParameterField pf = new ParameterField();
                ParameterFields pfs = new ParameterFields();
                ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                pf.Name = "@Curso";
                pf.Name = "@Seccion";
                pf.Name = "@Area";
                pdv.Value = txtcurso.Text;
                pdv.Value = txtseccion.Text;
                pdv.Value = txtarea.Text;
                pf.CurrentValues.Add(pdv);
                pfs.Add(pf);
                form.crystalReportViewer1.ParameterFieldInfo = pfs;
                oRep.Load(@"C:\Users\User\source\repos\ContAlumnos\ContAlumnos\CrystalReport1.rpt");
                form.crystalReportViewer1.ReportSource = oRep;
                form.Show();
            }

            else
            {
                MessageBox.Show("Por favor selecciona un curso, una seccion y una area", "Seleccion");
            }
        }
    }
}
