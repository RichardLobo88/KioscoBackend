namespace Kiosco.Models;

using Kiosco.Model;
using Kiosco.Models;

public class AuthenticateResponse
{
    public int Id { get; set; }
    //public string? FirstName { get; set; }
   // public string? LastName { get; set; }
    
    public string? Username { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        Id = user.ID;
        // FirstName = user.Username;
        //  LastName = user.LastName;
        Username = user.NombreUsuario;
        Token = token;
    }
}