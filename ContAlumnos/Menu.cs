using ContAlumnos.Clases.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos
{
    public partial class Menu : Form
    {
        static bool ocultar = true;
        static bool encender = false;

        public Menu()
        {
            InitializeComponent();
        }

        void hide()
        {
            if (ocultar == true)
            {
                btnInicio.Visible = false;
                btnestudiantes.Visible = false;
                btnmaestros.Visible = false;
                btnconfiguracion.Visible = false;
                btnSalir.Visible = false;

                panel2.Width = 170;

                ocultar = false;

                Point newLocation = btnocultar.Location;
                newLocation.X = 12;

                btnocultar.Location = newLocation;
            }

            else
            {
                panel2.Width = 262;

                btnInicio.Visible = true;
                btnestudiantes.Visible = true;
                btnmaestros.Visible = true;
                btnconfiguracion.Visible = true;
                btnSalir.Visible = true;

                ocultar = true;

                Point newLocation = btnocultar.Location;
                newLocation.X = 210;

                btnocultar.Location = newLocation;
            }
        }

        public void text(string tittle)
        {
            titulo.Text = tittle;
        }

        public void AbrirFormEnPanel(object Formhijo)
        {
            if (this.panelDesktop.Controls.Count > 0)
                this.panelDesktop.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(fh);
            this.panelDesktop.Tag = fh;
            fh.Show();
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void Menu_Load(object sender, EventArgs e)
        {
            bordesradius();
            btnInicio.PerformClick();
            btnuser.Text = Acceso.Nombre;
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            hide();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            text("Menu Principal");
            AbrirFormEnPanel(new Inicio());
        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            text("Manejo Estudiantes");
            AbrirFormEnPanel(new Estudiantes());
        }

        private void btnmaestros_Click(object sender, EventArgs e)
        {
            text("Manejo Maestros");
            AbrirFormEnPanel(new Inicio());
        }

        private void btnconfiguracion_Click(object sender, EventArgs e)
        {
            text("Configuracion");
            AbrirFormEnPanel(new Inicio());
        }

        private void bunifuFormControlBox1_HelpClicked(object sender, EventArgs e)
        {

        }

        private void bunifuFormControlBox1_CloseClicked(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            Visible = false;
            frm.Visible = true;
        }

        private void btnuser_Click(object sender, EventArgs e)
        {
            

            if(encender == false)
            {
                pUser.Visible = true;
                encender = true;
            }

            else
            {
                pUser.Visible = false;
                encender = false;
            }
        }
    }
}
