using LavaQuestDBAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaQuestDBAPI.Data.Repositories
{
    public interface iUsuariosRepository
    {
        Task<IEnumerable<Usuarios>> GetAllUsuarios();
        Task<Usuarios> GetDetails(string correo);
        Task<bool> InsertUsuario(Usuarios usuario);
        Task<bool> UpdateUsuario(Usuarios usuario);
        Task<bool> DeleteUsuario(Usuarios usuario);

    }
}
