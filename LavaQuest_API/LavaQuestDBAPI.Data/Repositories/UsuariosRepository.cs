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
    public class UsuariosRepository : iUsuariosRepository
    {
        //conexión con la base de datos
        private readonly MySQLConfiguration _connectionString;
        public UsuariosRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        //Función prueba para eliminar un usuario
        public Task<bool> DeleteUsuario(Usuarios usuario)
        {
            throw new NotImplementedException();
        }
        
          
        //Función prueba para obtener todos los usuarios existentes
        public async Task<IEnumerable<Usuarios>> GetAllUsuarios()
        {
            var db = dbConnection();

            var sql = @"SELECT idusuarios, Tipo, Nombre, Correo, contrasena FROM usuarios";

            return await db.QueryAsync<Usuarios>(sql, new { });
        }

        //Aquí obtenemos toda la información de un usuario determinado en base a su dirección de correo electrónico
        public async Task<Usuarios> GetDetails(string correo)
        {
            var db = dbConnection();

            var sql = @"SELECT idUsuarios, Tipo, Nombre, Correo, contrasena 
                        FROM usuarios 
                        WHERE Correo = @Correo";

            return await  db.QueryFirstOrDefaultAsync<Usuarios>(sql, new { Correo = correo });
        }
        
        //Función prueba para insertar un registro de usuario en la base de datos
        public async Task<bool> InsertUsuario(Usuarios usuario)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO Usuarios(Tipo, Nombre, Correo, Contrasena) VALUES (@Tipo, @Nombre, @Correo, @contrasena)";

            var result = await db.ExecuteAsync(sql, new
            { usuario.Tipo, usuario.Nombre, usuario.Correo, usuario.Contrasena });

            return result > 0;
        }

        //Función prueba para cambiar la información de un usuario
        public async Task<bool> UpdateUsuario(Usuarios usuario)
        {
            var db = dbConnection();

            var sql = @"UPDATE  usuarios
                        SET Tipo = @tipo, Nombre = @nombre, Correo = @correo, Contrasena = @contrasena WHERE idUsuario = @id";

            var result = await db.ExecuteAsync(sql, new
            { usuario.Tipo, usuario.Nombre, usuario.Correo, usuario.Contrasena });

            return result > 0;
        }
    }
}
