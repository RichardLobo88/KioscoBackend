using Dapper;
using Kiosco.Model;
using System.Data.SqlClient;

namespace Kiosco.Data
{
    public class UserData
    {
            private readonly string conection_string = "Server=DESKTOP-DKKNBKN;Database=Kiosco; Integrated Security = true; Trusted_Connection=Yes;";

            public List<User> GetAllLogines()
            {
                List<User> list = new List<User>();
                try
                {
                using (var cnn = new SqlConnection(conection_string))
                {
                        var query = "SELECT * FROM Usuarios";
                        list = cnn.Query<User>(query).ToList();
                    }
                }
                catch (Exception X) { }

                return list;

            }


            public User GetById(int id)
            {
                User list = new User();
                try
                {
                using (var cnn = new SqlConnection(conection_string))
                {
                        var query = "SELECT *FROM Usuarios where id = " + id;
                        list = cnn.Query<User>(query).FirstOrDefault();
                    }
                }
                catch (Exception X) { }

                return list;

            }

            public User GetLogines(string Usuario, string Contraseña)
            {
                var list = new User();
                try
                {
                using (var cnn = new SqlConnection(conection_string))
                {
                    var query = $"SELECT *FROM Usuario WHERE NombreUsuario = '{Usuario}' and Contraseña = '{Contraseña}'";
                        list = cnn.Query<User>(query).FirstOrDefault();
                    }
                }
                catch (Exception X) { }

                return list;

            }
    }
}
