using LavaQuestDBAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LavaQuestDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasController : Controller
    {
   
        private readonly iPreguntasRepository _preguntasRepository;

        public PreguntasController(iPreguntasRepository preguntasRepository)
        {
            _preguntasRepository = preguntasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPreguntas()
        {
            return Ok(await _preguntasRepository.GetAllPreguntas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPreguntasDetails(string id)
        {
            return Ok(await _preguntasRepository.GetDetails(id));
        }


    }
}
