using Kiosco.Data;
using Kiosco.Model;
using Microsoft.AspNetCore.Mvc;

namespace Kiosco.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Horariocontrollers : ControllerBase
     {
        [HttpGet("Obtener-todos-Horarios")]

        public IActionResult GetTodosHorarios()
        {
            var horarioData = new HorarioData();
           var resultado = horarioData.GetAllHorarios();
            if (resultado.resultado.Error)
            {
                return NotFound(resultado);
            }
            if (resultado.horarios.Any())
            {
                return NoContent();
            }
            return Ok(resultado);
        }


        [HttpPost("Insertar-horarios")]
        public IActionResult InsertHorarios(Horarios horarios)
        {
            var horarioData = new HorarioData();
            var horario = horarioData.InsertHorarios(horarios);
           
            if (horario.resultado.Error)
            {
                return NotFound(horario);
            }
            
            if (horario.horario == null)
            {
                return NoContent();
            }

            return Ok(horario);
        }
        [HttpPut("Actualizar")]
        public  IActionResult UpdateHorarios(Horarios Entradahorario)
        {
            var horarioData = new HorarioData();
            var horario = horarioData.UpdateHorarios(Entradahorario);

            if (horario.resultado.Error)
            {
                return NotFound(horario);
            }

            if (horario.horario == null)
            {
                return NoContent();
            }

            return Ok(horario);
        }


        [HttpDelete("Eliminar")]
        public IActionResult DeleteHorarios(int Entradahorario)
        {
            var horarioData = new HorarioData();
            var horario = horarioData.DeleteHorario(Entradahorario);

            if (horario.resultado.Error)
            {
                return NotFound(horario);
            }
                       
            return Ok(horario);
        }

    }


}
