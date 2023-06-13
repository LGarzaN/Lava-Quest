using LavaQuest_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace LavaQuest_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IList<Examen> listaExamen { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //Asignar el ID del usuario y su nombre a variables
            string id = HttpContext.Session.GetString("idUsuario");

            string nombreUsuario = HttpContext.Session.GetString("SNombre");

            //Si no existe una sesión, redirigir a la pagina de iniciar sesión
            if (nombreUsuario == null)
            {
                Response.Redirect("IniciarSesion");
            }

            //Crear una lista nueva de examenes para poder desplegarlos
            listaExamen = new List<Examen>();

            //String de conexión a la base de datos
            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            //abriendo conexion a la BD
            MySqlConnection conexion = new MySqlConnection(cs);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;


            //Query para leer todos los renglones de la tabla examen que haya creado el usuario activo

            if (id != null)
            {
                cmd.CommandText = "Select * from examen where idUsuario = " + id;

                //inicializar objeto examen y una lista de examenes
                Examen ex = new Examen();
                listaExamen = new List<Examen>();

                // Leer cada columna de la tabla y agregar el objeto a la lista
                // Esta lista se usara para desplegar los examenes existentes en la pagina
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

        //Al dar click en un examen, se le redirige al usuario a la pagina de resultados para mostrar los resultados de ese examen
        public IActionResult OnPostClick(string botonid)
        {
            TempData["ID"] = botonid;

            return RedirectToPage("Resultados");

        }

    }
}