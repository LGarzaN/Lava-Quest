using LavaQuest_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;

namespace LavaQuest_Web.Pages
{
    public class ResultadosModel : PageModel
    {
        public IList<Resultado> listaResultados { get; set; }

        public int boton { get; set; }



        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public void OnGet()
        {
            string nombreUsuario = HttpContext.Session.GetString("SNombre");

            if (nombreUsuario == null)
            {
                Response.Redirect("IniciarSesion");
            }

            string id = (string)TempData["id"];
            listaResultados = new List<Resultado>();

            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            //abriendo conexion a la BD
            MySqlConnection conexion = new MySqlConnection(cs);
            conexion.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;

            //Query para leer todos los renglones de la tabla resultados
            cmd.CommandText = "SELECT t1.*, t2.Nombre FROM resultados AS t1 JOIN usuarios AS t2 ON t1.idUsuario = t2.idUsuarios WHERE t1.idExamen = " + id;

            //inicializar objeto resultado y una lista de resultados
            Resultado res = new Resultado();
            listaResultados = new List<Resultado>();

            // Leer cada columna de la tabla y agregar el objeto a la lista
            // Esta lista se usara para desplegar los resultados 
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    res = new Resultado();
                    res.idResultados = Convert.ToInt32(reader["idResultados"]);
                    res.idExamen = Convert.ToInt32(reader["idExamen"]);
                    res.idUsuario = Convert.ToInt32(reader["idUsuario"]);
                    res.Puntaje = Convert.ToInt32(reader["Puntaje"]);
                    res.Nombre = reader["Nombre"].ToString();

                    listaResultados.Add(res);
                }
            }

            conexion.Close();
        }

        public void OnPost() 
        {

          
        }
    }
}
