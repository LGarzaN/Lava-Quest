using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace LavaQuest_Web.Pages
{
    public class ModificarUsuarioModel : PageModel
    {
        // Variables publicas

        [BindProperty]
        public string? Nombre { get; set; }

        [BindProperty]
        public string? Correo { get; set; }

        [BindProperty]
        public string? Contrasena { get; set; }

        [BindProperty]
        public string? confirmarContrasena { get; set; }

        public void OnGet()
        {
            string nombreUsuario = HttpContext.Session.GetString("SNombre");
            string correoUsuario = HttpContext.Session.GetString("SCorreo");
            string contrasenaUsuario = HttpContext.Session.GetString("SContrasena");

            TempData["NombreValue"] = nombreUsuario;
            TempData["CorreoValue"] = correoUsuario;
            TempData["ContrasenaValue"] = contrasenaUsuario;
            TempData["ConfirmarContrasenaValue"] = contrasenaUsuario;

            if (nombreUsuario == null)
            {
                Response.Redirect("IniciarSesion");
            }
        }

        // Metodo OnPost del formulario de modificar cuenta
        public IActionResult OnPost()
        {
            // Validación para ver si las contraseñas coinciden o no
            if (confirmarContrasena != Contrasena)
            {
                // Crea un mensaje de error con el que podremos poner el borde rojo de error a confirmar contraseña
                ModelState.AddModelError("ConfirmarContrasena", "Las contraseñas no coinciden");

                // Establece los valores de los campos del formulario en TempData para que se mantengan al redirigir la página
                TempData["NombreValue"] = Nombre;
                TempData["CorreoValue"] = Correo;
                TempData["ContrasenaValue"] = Contrasena;
                TempData["ConfirmarContrasenaValue"] = confirmarContrasena;

                return Page();
            }
            else
            {
                string correoUsuario = HttpContext.Session.GetString("SCorreo");
                // Elimina el ModelState de error
                ModelState.Remove("ConfirmarContrasena");

                string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

                using var con = new MySqlConnection(cs);
                con.Open();

                using var cmdModificar = new MySqlCommand();
                cmdModificar.Connection = con;

                cmdModificar.CommandText = "UPDATE usuarios SET Nombre = @Nombre, Contrasena = @Contrasena WHERE Correo = @Correo";
                cmdModificar.Parameters.AddWithValue("@Nombre", Nombre);
                cmdModificar.Parameters.AddWithValue("@Correo", correoUsuario);
                cmdModificar.Parameters.AddWithValue("@Contrasena", Contrasena);
                cmdModificar.ExecuteNonQuery();

                // Actualiza los valores del session por los modificador
                HttpContext.Session.SetString("SNombre", Nombre);
                HttpContext.Session.SetString("SContrasena", Contrasena);

                // Establece los valores de los campos del formulario en TempData para que se mantengan al redirigir la página
                TempData["NombreValue"] = Nombre;
                TempData["CorreoValue"] = correoUsuario;
                TempData["ContrasenaValue"] = Contrasena;
                TempData["ConfirmarContrasenaValue"] = confirmarContrasena;

                return Page();
            }
        }
    }
}
