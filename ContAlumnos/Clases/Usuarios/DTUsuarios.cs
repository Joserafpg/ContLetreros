using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContAlumnos.Clases.Usuarios
{
    public class DTUsuarios
    {
        public static bool activo = true;
        public static int Agregar(UsuariosGET pget)
        {

            int retorno = 0;

            Conexion.opencon();

            SqlCommand Comando = new SqlCommand(string.Format("Insert into Usuarios (Usuario, Contraseña, SuperUsuario, UsuarioComun, Activo) values ('{0}','{1}','{2}','{3}','{4}')",
                    pget.Usuario, pget.Contraseña, pget.SuperUsuario, pget.UsuarioComun, activo), Conexion.ObtenerConexion());

            retorno = Comando.ExecuteNonQuery();
            Conexion.cerrarcon();
            return retorno;

        }
    }
}
