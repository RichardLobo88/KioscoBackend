
using Dapper;
using Kiosco.Authorization;
using Kiosco.Models;
using Kiosco.Model;
using Kiosco.Data;
using System.Data.SqlClient;

namespace Kiosco.Services;


public class UserService 
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    public UserData user = new UserData();
    public const string conection_string = "Server=DESKTOP-DKKNBKN;Database=Kiosco; Integrated Security = true; Trusted_Connection=Yes;";
    private readonly IJwtUtils _jwtUtils;

    public UserService(IJwtUtils jwtUtils, UserData usuario)
    {
        _jwtUtils = jwtUtils;
        user = usuario;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        var _user = user.GetLogines(model.Username, model.Password);

        // return null if user not found
        if (_user == null) return null;

        // authentication successful so generate jwt token
        var token = _jwtUtils.GenerateJwtToken(_user);

        return new AuthenticateResponse(_user, token);
    }

    public List<User> GetAllLogines()
    {
        List<User> list = new List<User>();
        try
        {
            using (var cnn = new SqlConnection(conection_string))
            {
                var query = "SELECT *FROM Usuarios";
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
                var query = "SELECT * FROM Usuarios where id = " + id;
                list = cnn.Query<User>(query).FirstOrDefault();
            }
        }
        catch (Exception X) { }

        return list;

    }
}