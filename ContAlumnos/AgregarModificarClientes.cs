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
    public partial class AgregarModificarClientes : Form
    {
        public AgregarModificarClientes()
        {
            InitializeComponent();
        }

        public bool EditMode { get; set; }
        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContLetreros; Integrated Security=True");

        public void InitializeData(Int64 id, string nombre, string apellido, string cedula, string sexo, DateTime FechaIngreso)
        {
            txtnumero.Text = id.ToString();
            txtnombre.Text = nombre;
            txtapellido.Text = apellido;
            txtcedula.Text = cedula;
            cSexo.Text = sexo;
            fecha.Value = FechaIngreso;
        }
    
        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            if (EditMode)
            {
                txtnumero.Enabled = false;
                DatosgetClientes pEstudiantes = new DatosgetClientes();
                pEstudiantes.Numero = Convert.ToInt64(txtnumero.Text);
                pEstudiantes.Nombre = txtnombre.Text;
                pEstudiantes.Apellido = txtapellido.Text;
                pEstudiantes.Cedula = txtcedula.Text;
                pEstudiantes.Sexo = cSexo.Text;
                pEstudiantes.Fecha_Ingreso = fecha.Value;

                int Resultado = DatosbaseClientes.Modificar(pEstudiantes);

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
                if (!string.IsNullOrEmpty(txtnombre.Text) && !string.IsNullOrEmpty(txtapellido.Text) && !string.IsNullOrEmpty(cSexo.Text))
                {
                    DatosgetClientes pEstudiantes = new DatosgetClientes();
                    pEstudiantes.Nombre = txtnombre.Text;
                    pEstudiantes.Apellido = txtapellido.Text;
                    pEstudiantes.Cedula = txtcedula.Text;
                    pEstudiantes.Sexo = cSexo.Text;
                    pEstudiantes.Fecha_Ingreso = fecha.Value;

                    int Resultado = DatosbaseClientes.Agregar(pEstudiantes);

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

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgregarModificarClientes_Load(object sender, EventArgs e)
        {
            if (EditMode)
            {
                txtnumero.Visible = true;
                label1.Visible = true;
            }

            else
            {
                txtnumero.Visible = false;
                label1.Visible = false;
            }
        }
    }
}
