﻿using ContAlumnos.Clases.Estudiantes;
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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContLetreros; Integrated Security=True");

        void Buscar()
        {
            dataGridView1.DataSource = DatosbaseClientes.BuscarAlumnos(bunifuTextBox2.Text);
        }

        void CargarComboBox()
        {
            /*Conn.Open();
            string consulta = "SELECT DISTINCT Curso FROM Maestros";
            SqlCommand comando = new SqlCommand(consulta, Conn);
            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                txtcurso.Items.Add(lector.GetString(0));
            }

            Conn.Close();

            Conn.Open();
            string consulta2 = "SELECT DISTINCT Seccion FROM Maestros";
            SqlCommand comando2 = new SqlCommand(consulta2, Conn);
            SqlDataReader lector2 = comando2.ExecuteReader();

            while (lector2.Read())
            {
                txtseccion.Items.Add(lector2.GetString(0));
            }

            Conn.Close();

            Conn.Open();
            string consulta3 = "SELECT DISTINCT Area FROM Maestros";
            SqlCommand comando3 = new SqlCommand(consulta3, Conn);
            SqlDataReader lector3 = comando3.ExecuteReader();

            while (lector3.Read())
            {
                txtarea.Items.Add(lector3.GetString(0));
            }

            Conn.Close();*/
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
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[0].Value); // Suponiendo que el nombre de la columna que contiene el ID es "Numero"

                    Int64 resultado = DatosbaseClientes.Eliminar(id);
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
                string apellido = row.Cells[2].Value.ToString();
                string cedula = row.Cells[3].Value.ToString();
                string sexo = row.Cells[4].Value.ToString();
                DateTime fecha = Convert.ToDateTime(row.Cells[5].Value);

                // Abre el formulario para editar el producto
                AgregarModificarClientes formEditar = new AgregarModificarClientes();
                formEditar.EditMode = true; // Estás en modo editar
                formEditar.InitializeData(id, nombre, apellido, cedula, sexo, fecha);
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    // Actualiza el DataGridView después de la edición
                    Buscar();
                }
            }
        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            AgregarModificarClientes formAgregar = new AgregarModificarClientes();
            formAgregar.EditMode = false;
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                Buscar();
            }
        }

        private void Maestros_Load(object sender, EventArgs e)
        {
            Buscar();
            CargarComboBox();
        }

        private void txtcurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
