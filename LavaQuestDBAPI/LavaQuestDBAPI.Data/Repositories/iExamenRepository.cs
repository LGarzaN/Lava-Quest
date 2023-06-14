using LavaQuestDBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaQuestDBAPI.Data.Repositories
{
    public interface iExamenRepository
    {
        Task<IEnumerable<Examen>> GetAllExamenes();
        Task<Examen> GetDetails(string id);
    }
}
