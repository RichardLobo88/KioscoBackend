using Dapper;
using System.Data.SqlClient;
using Kiosco.Model;
using System.Collections.Generic;


namespace Kiosco.Data
{
    public class UsuarioData
    {
        private readonly string conection_string = "Server=DESKTOP-DKKNBKN;Database=Kiosco; Integrated Security = true; Trusted_Connection=Yes;";

        public List<UsuarioDTO> GetAllUsuario()
        {

            var list = new List<UsuarioDTO>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    var query = @"SELECT ID,Nombre,	Apellido, NombreUsuario, N_rol
                                    FROM usuario u 
                                   join roles r on r.id_roles = u.id_roles    ";

                    list = cnn.Query<UsuarioDTO>(query).ToList();
                }
            }
            catch (Exception X)
            {

            }
            return list;

        }

        public UsuarioDTO GetById(int id)
        {
            var usuario = new UsuarioDTO();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    var query = @$"SELECT ID,Nombre,	Apellido, NombreUsuario, N_rol
                                    FROM usuario u 
                                   join roles r on r.id_roles = u.id_roles  
                                where id = {id}";
                    usuario = cnn.Query<UsuarioDTO>(query).FirstOrDefault();
                }
            }
            catch (Exception X)
            {

            }

            return usuario;

        }
        // Obtener Logines
        public UsuarioDTO GetLogines(string Usuario, string Contraseña)
        {
            var list = new UsuarioDTO();
            try
            {
                using (SqlConnection cnn = new SqlConnection("Server=DESKTOP-DKKNBKN;Database=Kiosco; Integrated Security=true;Trusted_Connection=Yes"))
                {
                    var query = @$"SELECT ID,Nombre,	Apellido, NombreUsuario, N_rol
                                    FROM usuario u 
                                   join roles r on r.id_roles = u.id_roles 
                            WHERE Usuario = '{Usuario}' and Contraseña = '{Contraseña}'";
                    list = cnn.Query<UsuarioDTO>(query).FirstOrDefault();
                }
            }
            catch (Exception X) { }

            return list;

        }


        //Insertar Usuario
        public UsuarioDTO InsertUsuario(Usuario entradausuario)
        {
            var usuario = new UsuarioDTO();
            using (var cnn = new SqlConnection(conection_string))
            {
                var query = @"Insert into Usuario 
                            (Nombre, Apellido,NombreUsuario,Contraseña, id_roles)
                         values(@Nombre,@Apellido,@NombreUsuario,@Contraseña, @rol)";
                
                var result = cnn.Execute(query, entradausuario);
               
                query = @"SELECT Max(Id)
                    FROM Usuario";
                result = cnn.ExecuteScalar<int>(query);

                query = @$"SELECT ID,Nombre, Apellido, NombreUsuario, N_rol
                                    FROM usuario u 
                                   join roles r on r.id_roles = u.id_roles  
                                where id = {result}";

                usuario = cnn.Query<UsuarioDTO>(query).FirstOrDefault();

            }
            return usuario;
        }
    //Actualizar Usuarios
        public UsuarioDTO UpdateUsuario(Usuario entradausuario)
        {
            var usuario = new UsuarioDTO();
            using (var cnn = new SqlConnection(conection_string))
            {
                var query = $@"UPDATE Usuario SET   
                            Nombre=@Nombre, Apellido=@Apellido,NombreUsuario=@NombreUsuario,
                           Contraseña=@Contraseña, id_roles=@rol
                         where id=@ID";
                var result = cnn.Execute(query, entradausuario);

                query = @$"SELECT ID,Nombre, Apellido, NombreUsuario, N_rol
                                    FROM usuario u 
                                   join roles r on r.id_roles = u.id_roles  
                                where id = {entradausuario.ID}";

                usuario = cnn.Query<UsuarioDTO>(query).FirstOrDefault();

            }
            return usuario;
        }

// Borrar Usuarios
        public SalidaUsuario DeleteUsuario(int ID)
        {

            var usuario = new SalidaUsuario();
            try
            {
                using (var cnn = new SqlConnection(conection_string))
                {
                    var query = $@"UPDATE Usuarios SET   
                            eliminado = 0 where  id=@ID";

                    var result = cnn.Execute(query, ID);
                    usuario.resultado = new Resultado("OK");
                }

            }
            catch (Exception X)
            {

                usuario.resultado = new Resultado(X.Message);
                return usuario;

            }
            return usuario;

        }
    }
}
