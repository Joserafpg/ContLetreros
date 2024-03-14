using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Usuarios
{
    public class UsuariosGET
    {
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public bool SuperUsuario { get; set; }
        public bool UsuarioComun { get; set; }

        public UsuariosGET() { }

        public UsuariosGET(string usuario, string contraseña, bool superusuario, bool usercomun)
        {
            this.Usuario = usuario;
            this.Contraseña = contraseña;
            this.SuperUsuario = superusuario;
            this.UsuarioComun = usercomun;
        }
    }
}
