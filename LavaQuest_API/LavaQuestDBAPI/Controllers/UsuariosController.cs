using LavaQuestDBAPI.Data.Repositories;
using LavaQuestDBAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LavaQuestDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly iUsuariosRepository _usuariosRepository;

        public UsuariosController(iUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            return Ok(await _usuariosRepository.GetAllUsuarios());
        }

        [HttpGet("{correo}")]
        public async Task<IActionResult> GetUsuariosDetails(string correo)
        {
            return Ok(await _usuariosRepository.GetDetails(correo));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuarios usuario)
        {
            if (usuario == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _usuariosRepository.InsertUsuario(usuario);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario([FromBody] Usuarios usuario)
        {
            if (usuario == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _usuariosRepository.UpdateUsuario(usuario);

            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuariosRepository.DeleteUsuario(new Usuarios { idUsuarios = id});

            return NoContent();
        }
    }
}
