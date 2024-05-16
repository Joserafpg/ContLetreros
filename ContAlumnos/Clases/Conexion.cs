using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos.Clases
{
    public class Conexion
    {

        public static string computerName = Environment.MachineName;

        private static SqlConnection Conn = new SqlConnection($"Data source = {computerName}; Initial Catalog=ContLetreros; Integrated Security=True");

        public static SqlConnection ObtenerConexion()
        {
            return Conn;
        }

        public static void opencon()
        {
            Conn.Open();
        }

        public static void cerrarcon()
        {
            if (Conn != null)
            {
                try
                {
                    Conn.Close();
                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}