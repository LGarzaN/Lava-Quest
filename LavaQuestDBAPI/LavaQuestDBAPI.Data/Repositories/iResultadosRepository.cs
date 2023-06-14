using LavaQuestDBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaQuestDBAPI.Data.Repositories
{
    public interface iResultadosRepository
    {
            Task<Resultados> GetResultados(string id);
            Task<bool> InsertResultados(Resultados resultados);
    }
}


