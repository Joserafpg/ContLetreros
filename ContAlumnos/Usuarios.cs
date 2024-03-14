using ContAlumnos.Clases.Usuarios;
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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnestudiantes_Click(object sender, EventArgs e)
        {
            UsuariosGET pEstudiantes = new UsuariosGET();
            pEstudiantes.Usuario = txtusuario.Text;
            pEstudiantes.Contraseña = txtcontraseña.Text;
            pEstudiantes.SuperUsuario = chsp.Checked;
            pEstudiantes.UsuarioComun = chc.Checked;

            int Resultado = DTUsuarios.Agregar(pEstudiantes);

            if (Resultado > 0)
            {
                MessageBox.Show("Usuario agregado con exito", "Alumno modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("No se pudo agregar el usuario", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}
