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
using static System.Net.WebRequestMethods;

namespace ContAlumnos.Clases.Estudiantes
{
    public partial class AgregarModificarMaterias : Form
    {
        public AgregarModificarMaterias()
        {
            InitializeComponent();
        }

        public bool EditMode { get; set; }

        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContAlumnos; Integrated Security=True");

        public void InitializeData(string materia, string maestro, string curso, string seccion, string area)
        {
            txtmateria.Text = materia;
            txtmaestro.Text = maestro;
            cCurso.Text = curso;
            cSeccion.Text = seccion;
            cArea.Text = area;
        }

        void CargarComboBox()
        {
            Conn.Open();
            string consulta = "SELECT DISTINCT Nombre FROM Maestros";
            SqlCommand comando = new SqlCommand(consulta, Conn);
            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                txtmaestro.Items.Add(lector.GetString(0));
            }

            Conn.Close();
        }
               
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            if (EditMode)
            {
                DatosgetMaterias pEstudiantes = new DatosgetMaterias();
                pEstudiantes.Materia = txtmateria.Text;
                pEstudiantes.Maestro = txtmaestro.Text;
                pEstudiantes.Curso = cCurso.Text;
                pEstudiantes.Seccion = cSeccion.Text;
                pEstudiantes.Area = cArea.Text;

                int Resultado = DatosbaseMaterias.Modificar(pEstudiantes);

                if (Resultado > 0)
                {
                    MessageBox.Show("Alumno Modificado con exito", "Alumno modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No se pudo modificar el alumno", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // Indica que la operación fue exitosa
                this.DialogResult = DialogResult.OK;
                this.Close();
            }


            else
            {
                if (!string.IsNullOrEmpty(txtmateria.Text) && !string.IsNullOrEmpty(txtmaestro.Text) && !string.IsNullOrEmpty(cCurso.Text) && !string.IsNullOrEmpty(cSeccion.Text) && !string.IsNullOrEmpty(cArea.Text))
                {
                    DatosgetMaterias pEstudiantes = new DatosgetMaterias();
                    pEstudiantes.Materia = txtmateria.Text;
                    pEstudiantes.Maestro = txtmaestro.Text;
                    pEstudiantes.Curso = cCurso.Text;
                    pEstudiantes.Seccion = cSeccion.Text;
                    pEstudiantes.Area = cArea.Text;

                    int Resultado = DatosbaseMaterias.Agregar(pEstudiantes);

                    if (Resultado > 0)
                    {
                        MessageBox.Show("Alumno Agregado con exito", "Alumno agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el alumno", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    // Indica que la operación fue exitosa
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Por favor llena todo los campos", "Llenar campos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void AgregarModificarEstudiantes_Load(object sender, EventArgs e)
        {
            CargarComboBox();
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
