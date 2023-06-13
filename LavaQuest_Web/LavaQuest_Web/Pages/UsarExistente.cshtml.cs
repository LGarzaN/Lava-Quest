using LavaQuest_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;


namespace LavaQuest_Web.Pages
{
    public class UsarExistenteModel : PageModel
    {

        public IList<Examen> listaExamen { get; set; }

        public void OnGet()
        {
            string id = HttpContext.Session.GetString("idUsuario");

            string nombreUsuario = HttpContext.Session.GetString("SNombre");

            if (nombreUsuario == null)
            {
                Response.Redirect("IniciarSesion");
            }

            listaExamen = new List<Examen>();

            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            //abriendo conexion a la BD
            MySqlConnection conexion = new MySqlConnection(cs);
            conexion.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;

            //Query para leer todos los renglones de la tabla preguntas

            if (id != null)
            {
                cmd.CommandText = "Select * from examen where idUsuario = " + id;

                //inicializar objeto pregunta y una lista de preguntas
                Examen ex = new Examen();
                listaExamen = new List<Examen>();

                // Leer cada columna de la tabla y agregar el objeto a la lista
                // Esta lista se usara para desplegar las preguntas existentes en la pagina
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ex = new Examen();
                        ex.idExamen = Convert.ToInt32(reader["idExamen"]);
                        ex.Nombre = reader["Nombre"].ToString();
                        ex.idUsuario = Convert.ToInt32(reader["idUsuario"]);

                        listaExamen.Add(ex);
                    }
                }
            }
        }

        public IActionResult OnPostClick(string botonid)
        {
            TempData["ID"] = botonid;

            return RedirectToPage("MostrarCodigo");
        }
    }
}