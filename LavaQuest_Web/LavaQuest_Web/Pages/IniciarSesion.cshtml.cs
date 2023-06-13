using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace LavaQuest_Web.Pages
{
    public class IniciarSesionModel : PageModel
    {

        [BindProperty]
        public string? Correo { get; set; }

        [BindProperty]
        public string? Contrasena { get; set; }
        public string idUsuario { get; set; }

        public void OnGet()
        {
            // Elimina los elementos del session en caso de que existan
            HttpContext.Session.Remove("SNombre");
            HttpContext.Session.Remove("SCorreo");
            HttpContext.Session.Remove("SContrasena");
        }

        public IActionResult OnPost()
        {
            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            using var con = new MySqlConnection(cs);
            con.Open();

            // Verificar si el correo existe en la base de datos
            using var cmdVerificarCorreo = new MySqlCommand();
            cmdVerificarCorreo.Connection = con;
            cmdVerificarCorreo.CommandText = "SELECT COUNT(*) FROM usuarios WHERE Correo = @Correo";
            cmdVerificarCorreo.Parameters.AddWithValue("@Correo", Correo);
            int countCorreo = Convert.ToInt32(Convert.ToInt64(cmdVerificarCorreo.ExecuteScalar()));

            // Elimina los TempData de correo y contrasena en caso de que ya existan
            TempData.Remove("CorreoIncorrecto");
            TempData.Remove("ContrasenaIncorracta");

            if (countCorreo <= 0)
            {
                // El correo no está registrado
                ViewData["CorreoIncorrecto"] = "El correo que ingresaste no está conectado a una cuenta.";

                // Establece los valores de los campos del formulario en TempData para que se mantengan al redirigir la página
                TempData["CorreoValue"] = Correo;
                TempData["ContrasenaValue"] = Contrasena;

                return Page(); // Volver a la página de iniciar sesión
            }

            // Verificar si la contraseña coincide con el correo
            using var cmdVerificarContrasena = new MySqlCommand();
            cmdVerificarContrasena.Connection = con;
            cmdVerificarContrasena.CommandText = "SELECT COUNT(*) FROM usuarios WHERE Correo = @Correo AND Contrasena = @Contrasena";
            cmdVerificarContrasena.Parameters.AddWithValue("@Correo", Correo);
            cmdVerificarContrasena.Parameters.AddWithValue("@Contrasena", Contrasena);
            int countContrasena = Convert.ToInt32(Convert.ToInt64(cmdVerificarContrasena.ExecuteScalar()));

            if (countContrasena <= 0)
            {
                // La contraseña es incorrecta
                ViewData["ContrasenaIncorracta"] = "La contraseña que ingresaste es incorrecta.";

                // Establece los valores de los campos del formulario en TempData para que se mantengan al redirigir la página
                TempData["CorreoValue"] = Correo;
                TempData["ContrasenaValue"] = Contrasena;

                return Page(); // Volver a la página de iniciar sesión
            }

            // El correo y la contraseña son válidos
            using var cmdObtenerUsuario = new MySqlCommand();
            cmdObtenerUsuario.Connection = con;
            cmdObtenerUsuario.CommandText = "SELECT * FROM usuarios WHERE Correo = @Correo AND Contrasena = @Contrasena";
            cmdObtenerUsuario.Parameters.AddWithValue("@Correo", Correo);
            cmdObtenerUsuario.Parameters.AddWithValue("@Contrasena", Contrasena);

            using var reader = cmdObtenerUsuario.ExecuteReader();

            if (reader.Read())
            {

                HttpContext.Session.SetString("SNombre", reader.GetString("Nombre"));
                HttpContext.Session.SetString("SCorreo", reader.GetString("Correo"));
                HttpContext.Session.SetString("SContrasena", reader.GetString("Contrasena"));
            }

            //Guardar valor de idUsuario como variable de sesion
            con.Close();

            using var conect = new MySqlConnection(cs);
            conect.Open();

            using var cmnd = new MySqlCommand();
            cmnd.Connection = conect;

            cmnd.CommandText = "select idUsuarios from usuarios where Correo = @Correo";
            cmnd.Parameters.AddWithValue("@Correo", Correo);
            cmnd.ExecuteNonQuery();

            using (var read = cmnd.ExecuteReader())
            {
                while (read.Read())
                {
                    idUsuario = read["idUsuarios"].ToString();
                    break;
                }
            }

            HttpContext.Session.SetString("idUsuario", idUsuario);

            conect.Close();



            return RedirectToPage("Index");

        }   

    }
}
