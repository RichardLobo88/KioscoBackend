using System.Text.Json.Serialization;


namespace Kiosco.Model
{
    public class User
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }


        [JsonIgnore]
        public string? Contraseña { get; set; }
    }


}
