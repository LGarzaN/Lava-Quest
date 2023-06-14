using LavaQuestDBAPI.Data.Repositories;
using LavaQuestDBAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LavaQuestDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ResultadosController : ControllerBase
    {
        private readonly iResultadosRepository _resultadosRepository;
        public ResultadosController(iResultadosRepository resultadosRepository)
        {
            _resultadosRepository = resultadosRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResultados(string id)
        {
            return Ok(await _resultadosRepository.GetResultados(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Resultados resultado)
        {
            if (resultado == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _resultadosRepository.InsertResultados(resultado);

            return Created("created", created);

        }
    }
}
