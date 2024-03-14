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
    public partial class AgregarModificarMaestros : Form
    {
        public AgregarModificarMaestros()
        {
            InitializeComponent();
        }

        public bool EditMode { get; set; }

        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContAlumnos; Integrated Security=True");

        public void InitializeData(Int64 id, string nombre, string curso, string seccion, string area)
        {
            txtnumero.Text = id.ToString();
            txtnombre.Text = nombre;
            cCurso.Text = curso;
            cSeccion.Text = seccion;
            cArea.Text = area;
        }

       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            if (EditMode)
            {
                DatosgetMaestros pEstudiantes = new DatosgetMaestros();
                pEstudiantes.Nombre = txtnombre.Text;
                pEstudiantes.Curso = cCurso.Text;
                pEstudiantes.Seccion = cSeccion.Text;
                pEstudiantes.Area = cArea.Text;

                int Resultado2 = DatosbaseMaestros.Modificar(pEstudiantes);

                pEstudiantes.Numero = Convert.ToInt64(txtnumero.Text);
                int Resultado = DatosbaseMaestros.Modificar2(pEstudiantes);

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
                if (!string.IsNullOrEmpty(txtnombre.Text) && !string.IsNullOrEmpty(cCurso.Text) && !string.IsNullOrEmpty(cSeccion.Text) && !string.IsNullOrEmpty(cArea.Text))
                {
                    DatosgetMaestros pEstudiantes = new DatosgetMaestros();
                    pEstudiantes.Nombre = txtnombre.Text;
                    pEstudiantes.Curso = cCurso.Text;
                    pEstudiantes.Seccion = cSeccion.Text;
                    pEstudiantes.Area = cArea.Text;

                    int Resultado = DatosbaseMaestros.Agregar(pEstudiantes);
                    int Resultado2 = DatosbaseMaestros.Agregar2(pEstudiantes);

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
            if (EditMode)
            {
                txtnumero.Visible = true;
                txtnumero.Enabled = false;
            }

            else
            {
                txtnumero.Visible = false;
            }
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
