using Dapper;
using System.Data.SqlClient;

namespace Kiosco.Model
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string NombreUsuario { get; set; }

        public string Contraseña { get; set; }
        public int rol { get; set; }

    }

    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string n_rol { get; set; }


    }

    public class Horarios
    {
        public int? ID { get; set; }

        public int IDEmpleado { get; set; }
        public DateTime Fecha { get; set; }


        public DateTime HoraInicio { get; set; }

        public DateTime HoraFinalizacion { get; set; }

    }


    public class SalidaUsuario
    {
        public Usuario usuario { get; set; }

        public Resultado resultado { get; set; }


    }



}
