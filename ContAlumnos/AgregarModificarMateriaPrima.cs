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
    public partial class AgregarModificarMateriaPrima : Form
    {
        public AgregarModificarMateriaPrima()
        {
            InitializeComponent();
        }

        int numero = 0;
        string curso;
        string seccion;
        string area;

        public bool EditMode { get; set; }

        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContAlumnos; Integrated Security=True");

        public void InitializeData(Int64 id, string nombre, string apellido, string sexo, bool discapacidad, string curso, string seccion, string area)
        {
            txtnumero.Text = id.ToString();
            txtnombre.Text = nombre;
            txtapellido.Text = apellido;
            cCategoria.Text = sexo;
            cUnidadMedida.Text = curso;
        }

        private int Buscar(string curso, string seccion, string area)
        {
            int numeroMasAlto = 0;
            string query = "SELECT MAX(Numero) AS NumeroMaximo FROM Estudiantes WHERE Curso = @Curso AND Seccion = @Seccion AND Area = @Area";

            using (SqlCommand cmd = new SqlCommand(query, Conn))
            {
                // Agregar parámetros para prevenir la inyección SQL
                cmd.Parameters.AddWithValue("@Curso", curso);
                cmd.Parameters.AddWithValue("@Seccion", seccion);
                cmd.Parameters.AddWithValue("@Area", area);

                Conn.Open();
                // Ejecutar la consulta y obtener el número más alto
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    numeroMasAlto = Convert.ToInt32(result);
                }
                Conn.Close();
            }

            return numeroMasAlto;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            if (EditMode)
            {
                DatosgetInventario pEstudiantes = new DatosgetInventario();
                pEstudiantes.Numero = Convert.ToInt64(txtnumero.Text);
                pEstudiantes.Nombre = txtnombre.Text;
                pEstudiantes.Descripción = txtapellido.Text;
                pEstudiantes.Categoria = cCategoria.Text;
                pEstudiantes.Cantidad = Convert.ToDecimal(txtcantidad.Text);
                pEstudiantes.UnidadMedida = cUnidadMedida.Text;
                pEstudiantes.CostoUnitario = Convert.ToDecimal(txtcosto.Text);
                pEstudiantes.FechaCaducidad = caducidad.Value;
                pEstudiantes.FechaCompra = compra.Value;

                int Resultado = DatosbaseInventario.Modificar(pEstudiantes);

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
                if (/*!string.IsNullOrEmpty(txtnumero.Text) && !string.IsNullOrEmpty(txtnombre.Text) && !string.IsNullOrEmpty(txtapellido.Text) &&*/ !string.IsNullOrEmpty(cUnidadMedida.Text))
                {
                    DatosgetInventario pEstudiantes = new DatosgetInventario();
                    pEstudiantes.Nombre = txtnombre.Text;
                    pEstudiantes.Descripción = txtapellido.Text;
                    pEstudiantes.Categoria = cCategoria.Text;
                    pEstudiantes.Cantidad = 100m;
                    pEstudiantes.UnidadMedida = cUnidadMedida.Text;
                    pEstudiantes.CostoUnitario = 100m;
                    pEstudiantes.FechaCaducidad = caducidad.Value;
                    pEstudiantes.FechaCompra = compra.Value;

                    int Resultado = DatosbaseInventario.Agregar(pEstudiantes);

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

        private void cCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            curso = cUnidadMedida.Text;

            if (!string.IsNullOrEmpty(curso) && !string.IsNullOrEmpty(seccion) && !string.IsNullOrEmpty(area))
            {
                numero = Buscar(curso, seccion, area) + 1;
                txtnumero.Text = numero.ToString();
            }
        }

        private void AgregarModificarEstudiantes_Load(object sender, EventArgs e)
        {
            if (EditMode)
            {
                txtnumero.Enabled = true;
            }

            else
            {
                txtnumero.Enabled = false;
            }
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void compra_ValueChanged(object sender, EventArgs e)
        {

        }

        private void caducidad_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
