using ContAlumnos.Clases.Login;
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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        public static string computerName = Environment.MachineName;

        private void Inicio_Load(object sender, EventArgs e)
        {
            lname.Text = Acceso.Nombre;
            //ExecuteProcedureAndDisplayResult("CalcularTotalEstudiantes", total);
            //ExecuteProcedureAndDisplayResult("CalcularTotalHembras", hembras);
            //ExecuteProcedureAndDisplayResult("CalcularTotalVarones", varones);
        }

        private void ExecuteProcedureAndDisplayResult(string procedureName, Label textBox)
        {

            string connectionString = $"Server = {computerName}; database = ContAlumnos; Integrated Security = True";
            SqlConnection connection = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                var result = command.ExecuteScalar();
                connection.Close();

                if (result != null)
                {
                    textBox.Text = result.ToString();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lname_Click(object sender, EventArgs e)
        {

        }
    }
}
