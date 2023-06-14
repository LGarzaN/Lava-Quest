using LavaQuestDBAPI.Data;
using LavaQuestDBAPI.Data.Repositories;
using MySql.Data.MySqlClient;

namespace LavaQuestDBAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var mySQLConfiguration = new MySQLConfiguration(builder.Configuration.GetConnectionString("MySQLConnection"));
            builder.Services.AddSingleton(mySQLConfiguration);

            //para llamar una conexión al repositorio solamente cuando no exista - es más eficiente
            //builder.Services.AddSingleton(new MySqlConnection(builder.Configuration.GetConnectionString("MySQLConnection")));

            builder.Services.AddScoped<iUsuariosRepository, UsuariosRepository>();
            builder.Services.AddScoped<iExamenRepository, ExamenRepository>();
            builder.Services.AddScoped<iPreguntasRepository, PreguntasRepository>();
            builder.Services.AddScoped<iResultadosRepository, ResultadosRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}