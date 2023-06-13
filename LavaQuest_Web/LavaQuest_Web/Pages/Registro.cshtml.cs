using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using System;

namespace LavaQuest_Web.Pages
{
    public class RegistroModel : PageModel
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
        public string idUsuario { get; set; }

        public void OnGet()
        {
        }

        // Metodo OnPost del formulario de registro
        public IActionResult OnPost()
        {
            // Leer la opcion que eligió para tipo de usuario y la convierte a entero
            string opcion = Request.Form["option"];
            int TipoUsuario = int.Parse(opcion);

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
                // Elimina el ModelState de error
                ModelState.Remove("ConfirmarContrasena");

                string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

                using var con = new MySqlConnection(cs);
                con.Open();

                // Verificar si el correo ya existe en la tabla
                using var cmdVerificar = new MySqlCommand();
                cmdVerificar.Connection = con;
                cmdVerificar.CommandText = "SELECT COUNT(*) FROM usuarios WHERE Correo = @Correo";
                cmdVerificar.Parameters.AddWithValue("@Correo", Correo);
                int count = Convert.ToInt32(Convert.ToInt64(cmdVerificar.ExecuteScalar()));


                if (count > 0)
                {
                    // El correo ya existe en la base de datos
                    TempData["Error"] = "El correo ya está registrado";

                    // Establece los valores de los campos del formulario en TempData para que se mantengan al redirigir la página
                    TempData["NombreValue"] = Nombre;
                    TempData["CorreoValue"] = Correo;
                    TempData["ContrasenaValue"] = Contrasena;
                    TempData["ConfirmarContrasenaValue"] = confirmarContrasena;

                    return Page(); // Volver a la página de registro
                }
                else
                {
                    TempData.Remove("Error");

                    HttpContext.Session.SetString("SNombre", Nombre);
                    HttpContext.Session.SetString("SCorreo", Correo);
                    HttpContext.Session.SetString("SContrasena", Contrasena);

                    // El correo no existe en la base de datos, se puede proceder con la inserción
                    using var cmdInsertar = new MySqlCommand();
                    cmdInsertar.Connection = con;

                    cmdInsertar.CommandText = "INSERT INTO usuarios(idUsuarios, Tipo, Nombre, Correo, Contrasena) VALUES(NULL, @TipoUsuario, @Nombre, @Correo, @Contrasena)";
                    cmdInsertar.Parameters.AddWithValue("@Nombre", Nombre);
                    cmdInsertar.Parameters.AddWithValue("@Correo", Correo);
                    cmdInsertar.Parameters.AddWithValue("@Contrasena", Contrasena);
                    cmdInsertar.Parameters.AddWithValue("@TipoUsuario", TipoUsuario);
                    cmdInsertar.ExecuteNonQuery();

                    //Guardar valor de idUsuario como variable de sesion
                    using var cmnd = new MySqlCommand();
                    cmnd.Connection = con;

                    //Guardar el nombre del usuario como variable de sesión
                    cmnd.CommandText = "select idUsuarios from usuarios where Correo = @Correo";
                    cmnd.Parameters.AddWithValue("@Correo", Correo);
                    cmnd.ExecuteNonQuery();

                    using (var reader = cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idUsuario = reader["idUsuarios"].ToString();
                            break;
                        }

                    }

                    HttpContext.Session.SetString("idUsuario", idUsuario);

                    //Checar si el usuario es alumno o maestro
                    if (TipoUsuario == 1) return RedirectToPage("CuentaAlumno");
                    else { return RedirectToPage("Index"); }

                }
            }
        }
    }
}