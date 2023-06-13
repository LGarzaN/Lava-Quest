using LavaQuest_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace LavaQuest_Web.Pages
{
    public class MostrarCodigoModel : PageModel
    {

        public int boton { get; set; }
        public string Codigo { get; set; }

        public void OnGet()
        {
            string nombreUsuario = HttpContext.Session.GetString("SNombre");

            if (nombreUsuario == null)
            {
                Response.Redirect("IniciarSesion");
            }

            string id = (string)TempData["id"];

            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            //abriendo conexion a la BD
            MySqlConnection conexion = new MySqlConnection(cs);
            conexion.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;

            cmd.CommandText = "Select Codigo from examen where idExamen = " + id;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string codigo = reader.GetString("Codigo");

                    // Utiliza la variable 'codigo' para lo que necesites
                    Codigo = codigo;
                }
            }

            conexion.Close();
        }
    }
}
