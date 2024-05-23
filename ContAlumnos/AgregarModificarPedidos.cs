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
    public partial class AgregarModificarPedidos : Form
    {
        public AgregarModificarPedidos()
        {
            InitializeComponent();
        }

        public bool EditMode { get; set; }
        public static string computerName = Environment.MachineName;
        public static SqlConnection Conn = new SqlConnection($"Server = {computerName}; database=ContLetreros; Integrated Security=True");

        public void InitializeData(Int64 id, string nombre, string apellido, string cedula, string sexo, DateTime FechaIngreso)
        {
            /*txtnumero.Text = id.ToString();
            txtnombre.Text = nombre;
            txtapellido.Text = apellido;
            txtcedula.Text = cedula;
            cSexo.Text = sexo;
            fecha.Value = FechaIngreso;*/
        }
    }
}
