using Dapper;
using LavaQuestDBAPI.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaQuestDBAPI.Data.Repositories
{
    public class ExamenRepository : iExamenRepository
    {
        private readonly MySQLConfiguration _connectionString;

        //Creamos una conexión con la base de datos
        public ExamenRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        //Realizamos un query a la base de datos que nos retorna todos los examenes existentes
        public async Task<IEnumerable<Examen>> GetAllExamenes()
        {
            var db = dbConnection();

            var sql = @"SELECT idExamen, idUsuario, Nombre FROM examen";

            return await db.QueryAsync<Examen>(sql, new { });
        }

        //Obtenemos un examen específico en base al código proporcionado
        public async Task<Examen> GetDetails(string id)
        {
            var db = dbConnection();

            var sql = @"SELECT idExamen, idUsuario, Nombre 
                        FROM examen
                        WHERE Codigo = @Id";

            return await db.QueryFirstOrDefaultAsync<Examen>(sql, new { Id = id });
        }

    }
}
