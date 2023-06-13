using LavaQuest_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Encoders;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Http;

namespace LavaQuest_Web.Pages
{
    public class NomExamenModel : PageModel
    {
        [BindProperty]
        public string Nombre { get; set; }
        public int idEx { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //Definir variables
            string idUsuario = HttpContext.Session.GetString("idUsuario");
            int id;
            string codExam;

            // String de conexion a la base de datos
            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            //Establecer conexión nueva a la base de 
            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmnd = new MySqlCommand();
            cmnd.Connection = con;

            // Generar un código de 6 caracteres alfanuméricos
            codExam = GenerarCodigoAleatorio();

            // Verificar si el código existe en la base de datos
            while (ExisteCodigoEnBD(codExam, cmnd))
            {
                codExam = GenerarCodigoAleatorio();
            }

            //Agregar el examen nuevo a la base de datos
            cmnd.CommandText = "INSERT INTO examen(idExamen, idUsuario, Nombre, Codigo) VALUES(NULL, @idUsuario, @Nombre, @Code)";
            cmnd.Parameters.AddWithValue("@Nombre", Nombre);
            cmnd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmnd.Parameters.AddWithValue("@Code", codExam);
            cmnd.ExecuteNonQuery();

            //Seleccionar el ID del examen que se acaba de crear para poder asignar ese ID a las preguntas
            cmnd.CommandText = "select idExamen from examen where idExamen = (select max(idExamen) from examen)";
            cmnd.ExecuteNonQuery();

            using (var reader = cmnd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["idExamen"]);
                    idEx = id;
                    break;
                }

            }
            HttpContext.Session.SetInt32("idExamen", idEx);


            return RedirectToPage("/CrearExamen");


        }

        private string GenerarCodigoAleatorio()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string codigo = new string(
                Enumerable.Repeat(caracteres, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return codigo;
        }

        private bool ExisteCodigoEnBD(string codigo, MySqlCommand cmnd)
        {
            cmnd.CommandText = "SELECT COUNT(*) FROM examen WHERE Codigo = @Codigo";
            cmnd.Parameters.Clear();
            cmnd.Parameters.AddWithValue("@Codigo", codigo);
            int count = Convert.ToInt32(Convert.ToInt64(cmnd.ExecuteScalar()));
            return count > 0;
        }

    }
}