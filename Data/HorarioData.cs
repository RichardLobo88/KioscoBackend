using Dapper;
using Kiosco.Model;
using System.Data.SqlClient;

namespace Kiosco.Data
{
    public class HorarioData
    {
        private readonly string conection_string = "Server=DESKTOP-DKKNBKN;Database=Kiosco; Integrated Security = true; Trusted_Connection=Yes;";
        public SalidaListaHorarios GetAllHorarios()
        {
            var list = new SalidaListaHorarios();

            try
            {
                using (var cnn = new SqlConnection(conection_string))
                {
                    string query = "SELECT * FROM horarios where Eliminado = 1";
                    list.horarios = cnn.Query<Horarios>(query).ToList();
                    list.resultado=new Resultado("OK");

                }
            }
            catch (Exception X) { 
                list.resultado = new Resultado(X.Message);
                return list;
            }
            return list;
        }


        // obtener horarios por Usuarios
        public SalidaListaHorarios GetHorariosPorUsuario(int ID)
        {
            //List<Horarios> list = new List<Horarios>();
            var list = new SalidaListaHorarios();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    string query = $"SELECT IDEmpleado,Fecha FROM Horarios Where Eliminado = 1 and IDEmpleado={ID}";
                    list.horarios = cnn.Query<Horarios>(query).ToList();
                    list.resultado=new Resultado("OK");
                }
            }
            catch (Exception X) {
                list.resultado = new Resultado(X.Message);
            }
            return list;
        }
        
        //Obtener horarios
        public List<Horarios> ObtHorario()
        {
            List<Horarios> list = new List<Horarios>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    string query = "select * from Horarios where Fecha > GETDATE() order by id asc";
                    list = cnn.Query<Horarios>(query).ToList();
                }
            }
            catch (Exception X) { }
            return list;
        }
       
        //Insertar horarios
        public SalidaHorarios InsertHorarios(Horarios horarios)
        {
            var horario = new SalidaHorarios();

            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    string format = "yyyy-MM-dd HH:mm:ss";
                    
                    var formatedo= horarios.Fecha.ToString(format);
                    var formatedo1 = horarios.HoraInicio.ToString(format);
                    var formatedo2 = horarios.HoraFinalizacion.ToString(format);
                    var T = DateTime.Parse(horarios.Fecha.ToString("d"));
                  
                    var dia = horarios.HoraInicio.ToString("d");
                    horarios.Fecha = DateTime.Parse(T.ToString(format));



                    //string format = "yyyy-MM-dd HH:mm:ss";
                    string query = @"Insert into Horarios 
                (IDEmpleado,Fecha,HoraInicio, HoraFinalizacion,eliminado) Values
                    (" + horarios.IDEmpleado + ",'" + formatedo + "'," +
                    "'" + formatedo1 + "','" + formatedo2 + "',1)";
                    cnn.Query(query);
                    // horarios.HoraInicio.h = DateTime.Parse(horarios.HoraInicio.ToString("yyyy/MM/dd HH:mm:ss"));
                    // horarios.HoraFinalizacion = DateTime.Parse(horarios.HoraFinalizacion.ToString("yyyy/MM/dd HH:mm:ss"));
               //     string query = @$"Insert into Horarios 
               //(IDEmpleado,fecha) 
               //Values ({horarios.IDEmpleado},
               //{formatedo})";

               //     cnn.Execute(query);
                   
                    query = @"SELECT Max(Id)
                    FROM Horarios";

                    var result = cnn.ExecuteScalar<int>(query);

                    query = @$"SELECT *
                                    FROM horarios u 
                                   
                                where Eliminado = 1 and id = {result}";
                    horario.horario = cnn.Query<Horarios>(query).FirstOrDefault();
                    horario.resultado = new Resultado("OK");
                }
            }

            catch (Exception X) {
                horario.resultado = new Resultado(X.Message);
            }

            return horario;
        }

        public Horarios SelHorario(int ID_turno)
        {
            Horarios horarios = new Horarios();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    string query = "SELECT * FROM Horarios WHERE  Eliminado = 1 and ID =" + ID_turno;
                    horarios = cnn.Query<Horarios>(query).FirstOrDefault();
                }
            }
            catch (Exception X) 
            { 
            }
            return horarios;
        }
        public Horarios UpDateTurno(Horarios horarios)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    string query = "UPDATE Horarios SET UsuarioId = " +horarios.IDEmpleado + "WHERE  Id = " + horarios.ID;
                    cnn.Query(query);
                }
            }
            catch (Exception X) { }
            return horarios;
        }
        public List<Horarios> HorariosPorUsuario(int usuarioId)
        {
            List<Horarios> list = new List<Horarios>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    string query = "SELECT * FROM Turnos WHERE Eliminado = 1 and usuarioID=" + usuarioId;
                    list = cnn.Query<Horarios>(query).ToList();
                }
            }
            catch (Exception X) { }
            return list;
        }
        public SalidaListaHorarios Horarios_Por_Usuario(int usuarioId)
        {   //Peluqueros disponibles
            var list = new SalidaListaHorarios();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conection_string))
                {
                    string query = "SELECT * FROM Horarios WHERE Eliminado = 1 and IDEmpleado=" + usuarioId;
                    list.horarios = cnn.Query<Horarios>(query).ToList();
                    list.resultado = new Resultado("OK");
                }
            }
            catch (Exception X)
            {

           list.resultado = new Resultado(X.Message);
                return list;

            }
            return list;

        }

        public SalidaHorarios UpdateHorarios(Horarios Entradahorario)
        {
            var horario = new SalidaHorarios();
            try
            {
                using (var cnn = new SqlConnection(conection_string))
                {
                    var query = $@"UPDATE Horarios SET   
                            IDEmpleado=@IDEmpleado, Fecha=@Fecha,HoraInicio=@HoraInicio,
                           HoraFinalizacion=@HoraFinalizacion
                         where id=@ID";
                    var result = cnn.Execute(query, Entradahorario);

                    query = @$"SELECT * FROM Horarios 
                                   
                                where id = {Entradahorario.ID}";

                    horario.horario = cnn.Query<Horarios>(query).FirstOrDefault();
                    horario.resultado = new Resultado("OK");
                }

            }
            catch (Exception X) {

                horario.resultado = new Resultado(X.Message);
                return horario;

            }
            return horario;



        }


    public SalidaHorarios DeleteHorario(int ID) {

            var horario = new SalidaHorarios();
            try
            {
                using (var cnn = new SqlConnection(conection_string))
                {
                    var query = $@"UPDATE Horarios SET   
                            eliminado = 0 where  id=@ID";

                    var result = cnn.Execute(query, ID);
                    horario.resultado = new Resultado("OK");
                }

            }
            catch (Exception X)
            {

                horario.resultado = new Resultado(X.Message);
                return horario;

            }
            return horario;

        }
    }
   

}
