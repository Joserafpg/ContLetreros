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
    public partial class Estudiantes : Form
    {
        public Estudiantes()
        {
            InitializeComponent();
        }

        public static SqlConnection Conn = new SqlConnection("Server = DESKTOP-NDDA7LS; database=ContAlumnos; Integrated Security=True");

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

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DatosbaseEstudiantes.BuscarAlumnos(txtcurso.Text, txtseccion.Text, txtarea.Text);
        }

        private void Estudiantes_Load(object sender, EventArgs e)
        {
            CargarComboBox();
            dataGridView1.DataSource = DatosbaseEstudiantes.BuscarAlumnos(txtcurso.Text, txtseccion.Text, txtarea.Text);
        }

        private void txtcurso_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtcurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DatosbaseEstudiantes.BuscarAlumnos(txtcurso.Text, txtseccion.Text, txtarea.Text);
        }
    }
}
