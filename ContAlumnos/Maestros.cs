using ContAlumnos.Clases.Estudiantes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos
{
    public partial class Maestros : Form
    {
        public Maestros()
        {
            InitializeComponent();
        }

        void Buscar()
        {
            dataGridView1.DataSource = DatosbaseMaestros.BuscarAlumnos(txtcurso.Text, txtseccion.Text, txtarea.Text);
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debes seleccionar un estudiantes", "Seleccionar un estudiante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Esta seguro que desea eliminar el estudiante actual??", "Esta Seguro?!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value); // Suponiendo que el nombre de la columna que contiene el ID es "Numero"

                    Int64 resultado = DatosbaseMaestros.Eliminar(id);
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

        private void bunifuButton24_Click(object sender, EventArgs e)
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
                string curso = row.Cells[2].Value.ToString();
                string seccion = row.Cells[3].Value.ToString();
                string area = row.Cells[4].Value.ToString();

                // Abre el formulario para editar el producto
                AgregarModificarMaestros formEditar = new AgregarModificarMaestros();
                formEditar.EditMode = true; // Estás en modo editar
                formEditar.InitializeData(id, nombre, curso, seccion, area);
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    // Actualiza el DataGridView después de la edición
                    Buscar();
                }
            }
        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            AgregarModificarMaestros formAgregar = new AgregarModificarMaestros();
            formAgregar.EditMode = false;
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                Buscar();
            }
        }

        private void Maestros_Load(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
