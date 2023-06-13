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
            //verificar que exista una sesión
            string nombreUsuario = HttpContext.Session.GetString("SNombre");

            if (nombreUsuario == null)
            {
                Response.Redirect("IniciarSesion");
            }

            string id = (string)TempData["id"];

            //String de conexión a la base de datos
            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            //abriendo conexion a la BD
            MySqlConnection conexion = new MySqlConnection(cs);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;

            //Buscar el código del examen que se desea aplicar
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
