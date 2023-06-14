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
    public class ResultadosRepository : iResultadosRepository
    {
        //Conexión con la base de datos
        private readonly MySQLConfiguration _connectionString;
        public ResultadosRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        //Función prueba en la que obtenemos los registrados de un usuario en base a su id mediante el query correspondiente
        public async Task<Resultados> GetResultados(string id)
        {
            var db = dbConnection();

            var sql = @"SELECT idUsuario, idExamen, Puntaje
                        FROM Resultados 
                        WHERE idUsuario = @Id";

            return await db.QueryFirstOrDefaultAsync<Resultados>(sql, new { Id = id});
        }

        //Función en la que insertamos un registro de resultado mediante un objeto resultado recibido a través de la aplicació de Unity
        public async Task<bool> InsertResultados(Resultados resultado)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO resultados( idUsuario, idExamen, Puntaje) VALUES (@IdUsuario, @IdExamen, @Puntaje)";

            var result = await db.ExecuteAsync(sql, new
            {resultado.idUsuario, resultado.idExamen, resultado.Puntaje});

            return result > 0;
        }
    }
}
