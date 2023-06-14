using LavaQuestDBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaQuestDBAPI.Data.Repositories
{
    public interface iPreguntasRepository
    {
        Task<IEnumerable<Preguntas>> GetAllPreguntas();
        Task<IEnumerable<Preguntas>> GetDetails(string id);
    }
}
