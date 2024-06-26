﻿using ContAlumnos.Clases.Login;
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
                btnventas.Visible = false;
                btnconfiguracion.Visible = false;
                btnSalir.Visible = false;

                panel2.Width = 170;

                ocultar = false;

                Point newLocation = btnocultar.Location;
                newLocation.X = 12;

                btnocultar.Location = newLocation;




                Point newLocation2 = pUser.Location;
                newLocation2.X = 858;

                pUser.Location = newLocation2;
            }

            else
            {
                panel2.Width = 262;

                btnInicio.Visible = true;
                btnestudiantes.Visible = true;
                btnmaestros.Visible = true;
                btnventas.Visible = true;
                btnconfiguracion.Visible = true;
                btnSalir.Visible = true;

                ocultar = true;

                Point newLocation = btnocultar.Location;
                newLocation.X = 210;

                btnocultar.Location = newLocation;


                Point newLocation2 = pUser.Location;
                newLocation2.X = 950;

                pUser.Location = newLocation2;
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

        private void Menu_Load(object sender, EventArgs e)
        {
            btnInicio.PerformClick();
            btnuser.Text = Acceso.Nombre;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
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
            text("Inventario");
            AbrirFormEnPanel(new InventarioSelect());
        }

        private void btnmaestros_Click(object sender, EventArgs e)
        {
            text("Clientes");
            AbrirFormEnPanel(new Clientes());
        }

        private void btnconfiguracion_Click(object sender, EventArgs e)
        {
            text("Ventas");
            AbrirFormEnPanel(new Ventas());
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

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            text("Configuracion");
            AbrirFormEnPanel(new Configuracion());
        }
    }
}
