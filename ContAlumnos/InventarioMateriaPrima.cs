﻿using ContAlumnos.Clases.Estudiantes;
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
using static Bunifu.UI.WinForms.BunifuSnackbar;

namespace ContAlumnos
{
    public partial class InventarioMateriaPrima : Form
    {
        public InventarioMateriaPrima()
        {
            InitializeComponent();
        }

        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContLetreros; Integrated Security=True");

        void CargarComboBox()
        {
            Conn.Open();
            string consulta = "SELECT DISTINCT Categoría FROM Inventario";
            SqlCommand comando = new SqlCommand(consulta, Conn);
            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                txtdepartamento.Items.Add(lector.GetString(0));
            }

            Conn.Close();

            Conn.Open();
            string consulta2 = "SELECT DISTINCT UnidadMedida FROM Inventario";
            SqlCommand comando2 = new SqlCommand(consulta2, Conn);
            SqlDataReader lector2 = comando2.ExecuteReader();

            while (lector2.Read())
            {
                txtunidad.Items.Add(lector2.GetString(0));
            }

            Conn.Close();
        }

        void Buscar()
        {
            dataGridView1.DataSource = DatosbaseInventario.BuscarAlumnos(txtdepartamento.Text, txtunidad.Text, txtnombre.Text);
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
                Int64 id = Convert.ToInt64(row.Cells[0].Value);
                string nombre = row.Cells[1].Value.ToString();
                string descripcion = row.Cells[2].Value.ToString();
                string categoria = row.Cells[3].Value.ToString();
                int cantidad = Convert.ToInt32(row.Cells[4].Value);
                string unidadmedida = row.Cells[5].Value.ToString();
                decimal costounitario = Convert.ToDecimal(row.Cells[6].Value.ToString());
                DateTime fechacompra = Convert.ToDateTime(row.Cells[7].Value.ToString());
                DateTime fechacaducidad = Convert.ToDateTime(row.Cells[8].Value.ToString());

                // Abre el formulario para editar el producto
                AgregarModificarMateriaPrima formEditar = new AgregarModificarMateriaPrima();
                formEditar.EditMode = true; // Estás en modo editar
                formEditar.InitializeData(id, nombre, descripcion, categoria, cantidad, unidadmedida, costounitario, fechacompra, fechacaducidad);
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
                    int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value); // Suponiendo que el nombre de la columna que contiene el ID es "Numero"
                    
                    Int64 resultado = DatosbaseInventario.Eliminar(id);
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
            AgregarModificarMateriaPrima formAgregar = new AgregarModificarMateriaPrima();
            formAgregar.EditMode = false;
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                Buscar();
            }
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtdepartamento.Text) && !string.IsNullOrEmpty(txtunidad.Text))
            {
                /*PlantillaReporte form = new PlantillaReporte();
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
                oRep.Load(@"C:\Users\User\source\repos\ContAlumnos\ContAlumnos\Reportes de cursos.rpt");
                form.crystalReportViewer1.ReportSource = oRep;
                form.Show();
            }

            else
            {
                MessageBox.Show("Por favor selecciona un curso, una seccion y una area", "Seleccion");*/
            }
        }
    }
}
