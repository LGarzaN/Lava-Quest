using LavaQuestDBAPI.Data.Repositories;
using LavaQuestDBAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LavaQuestDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly iExamenRepository _examenRepository;

        public ExamenController (iExamenRepository examenRepository)
        {
            _examenRepository = examenRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExamenes()
        {
            return Ok(await _examenRepository.GetAllExamenes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamenDetails(string id)
        {
            return Ok(await _examenRepository.GetDetails(id));
        }

    }
}
