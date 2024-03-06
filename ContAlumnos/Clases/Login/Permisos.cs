using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Login
{
    public class Permisos
    {
        public Permisos()
        {
        }

        public static bool SuperUsuario { set; get; }
        public static bool UsuarioComun { set; get; }
    }
}
