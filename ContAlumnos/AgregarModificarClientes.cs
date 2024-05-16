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


        private void btnestudiantes_Click(object sender, EventArgs e)
        {

        }
    }
}
