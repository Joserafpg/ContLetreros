using ContAlumnos.Clases.Estudiantes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContAlumnos
{
    public partial class InventarioSelect : Form
    {
        public InventarioSelect()
        {
            InitializeComponent();
        }

        public void AbrirFormEnPanel(object Formhijo)
        {
            if (this.panel1.Controls.Count > 0)
                this.panel1.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(fh);
            this.panel1.Tag = fh;
            fh.Show();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            AbrirFormEnPanel(new Inventario());
        }
    }
}
