using Kiosco.Data;
using Microsoft.AspNetCore.Mvc;
using Kiosco.Model;
using Dapper;
using System.Data.SqlClient;

using Microsoft.EntityFrameworkCore;

namespace Kiosco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TestControllers : ControllerBase
    {
        [HttpGet("Obtener-todos-Usuarios")]

        public List<UsuarioDTO> GetTodosUsuarios()
        {
            var usuarioData = new UsuarioData();
            return usuarioData.GetAllUsuario();
        }

        [HttpGet("Obtener-por-id-Usuario/{id}")]
        public UsuarioDTO GetPorIdUsuario(int id)
        {
            var usuarioData = new UsuarioData();
            return usuarioData.GetById(id);
        }


        [HttpPost("Insertar-usuario")]
        public IActionResult InsertUsuario(Usuario registroModel)
        {
            var usuarioData = new UsuarioData();
            var usuario = usuarioData.InsertUsuario(registroModel);
            return Ok(usuario);
        }

        [HttpPut("Actualizar-Usuario")]
        public IActionResult UpdateUsuario(Usuario registroModel)
        {
            var usuarioData = new UsuarioData();
            var usuario = usuarioData.UpdateUsuario(registroModel);
            return Ok(usuario);
        }


        [HttpDelete("Eliminar")]
        public IActionResult DeleteUsuarios(int EntradaUsuario)
        {
            var usuarioData = new UsuarioData();
            var usuario = usuarioData.DeleteUsuario(EntradaUsuario);

            if (usuario.resultado.Error)
            {
                return NotFound(usuario);
            }

            return Ok(usuario);
        }





    }
}
