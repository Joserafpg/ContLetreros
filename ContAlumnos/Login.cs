using ContAlumnos.Clases;
using ContAlumnos.Clases.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        void bordesradius()
        {
            int borderRadius = 20;
            GraphicsPath objDraw = new GraphicsPath();

            objDraw.AddArc(0, 0, borderRadius * 2, borderRadius * 2, 180, 90);
            objDraw.AddArc(this.Width - borderRadius * 2, 0, borderRadius * 2, borderRadius * 2, 270, 90);
            objDraw.AddArc(this.Width - borderRadius * 2, this.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            objDraw.AddArc(0, this.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            objDraw.CloseFigure();

            this.Region = new Region(objDraw);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Login_Load(object sender, EventArgs e)
        {
            bordesradius();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            if (txtuser.Text.Equals(""))
            {
                MessageBox.Show("El usuario no debe estar en blanco!...");
                txtuser.Focus();
                return;
            }

            if (txtpass.Text.Equals(""))
            {
                MessageBox.Show("La contraseña no debe estar en blanco!...");
                txtpass.Focus();
                return;
            }

            DataTable dt = new DataTable();
            string consulta;
            consulta = "SELECT * FROM Usuarios WHERE Usuario = @usuario AND Contraseña = @contrasena"; // Verificar si el usuario existe
            Conexion.opencon();
            SqlDataAdapter da = new SqlDataAdapter(consulta, Conexion.ObtenerConexion());

            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.Add("@usuario", SqlDbType.VarChar, 15).Value = txtuser.Text;
            da.SelectCommand.Parameters.Add("@contrasena", SqlDbType.VarChar, 15).Value = txtpass.Text;

            da.Fill(dt);

            Conexion.cerrarcon();

            if (dt.Rows.Count > 0)
            {
                Int64 id = Convert.ToInt32((Int32)dt.Rows[0]["Id_Usuario"]);
                string usuario = (string)dt.Rows[0]["Usuario"];
                string contraseña = (string)dt.Rows[0]["Contraseña"];

                Acceso.ID = id;
                Acceso.Nombre = usuario;
                Acceso.Contraseña = contraseña;

                bool estaActivo = (bool)dt.Rows[0]["Activo"];
                if (!estaActivo)
                {
                    MessageBox.Show("El usuario está suspendido.", "Usuario suspendido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    Permisos.SuperUsuario = Convert.ToBoolean(dt.Rows[0][4]);
                    Permisos.UsuarioComun = Convert.ToBoolean(dt.Rows[0][5]);

                    //Permisos.Administrador = Convert.ToBoolean(dt.Rows[0][7]);
                    //Permisos.Inventario = Convert.ToBoolean(dt.Rows[0][8]);

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            Console.WriteLine($"{col.ColumnName}: {row[col]}");
                        }
                        Console.WriteLine("--------------------------------");
                    }

                    Menu principal = new Menu();
                    principal.FormClosed += (s, args) => Application.Exit();
                    principal.Show();
                    principal.Visible = true;
                    Visible = false;
                }
            }

            else
            {
                MessageBox.Show("Usuario o contraseña incorrecto.");
                txtpass.Focus();
                txtuser.BorderColorActive = Color.Red;
                txtuser.BorderColorDisabled = Color.Red;
                txtuser.BorderColorHover = Color.Red;
                txtuser.BorderColorIdle = Color.Red;

                txtpass.BorderColorActive = Color.Red;
                txtpass.BorderColorDisabled = Color.Red;
                txtpass.BorderColorHover = Color.Red;
                txtpass.BorderColorIdle = Color.Red;
            }
        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }
    }
}
