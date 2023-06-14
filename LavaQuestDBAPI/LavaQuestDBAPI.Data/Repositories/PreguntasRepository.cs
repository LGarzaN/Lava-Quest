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
    public class PreguntasRepository : iPreguntasRepository
    {
        //Creamos la conexión con la base de datos
        private readonly MySQLConfiguration _connectionString;

        public PreguntasRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        //Función de prueba para obtener todas las preguntas registradas mediante un query
        public async Task<IEnumerable<Preguntas>> GetAllPreguntas()
        {
            var db = dbConnection();

            var sql = @"SELECT idPregunta, idExamen, pregunta, respuesta1, respuesta2, respuesta3, respuesta4, correcta FROM Preguntas";

            return await db.QueryAsync<Preguntas>(sql, new { });
        }

        //Obtenemos todas las preguntas correspondientes a un id de exámen
        public async Task<IEnumerable<Preguntas>> GetDetails(string id)
        {
            var db = dbConnection();

            var sql = @"SELECT * 
                        FROM Preguntas
                        WHERE idExamen = @Id";

            return await db.QueryAsync<Preguntas>(sql, new { Id = id });
        }

    }
}
