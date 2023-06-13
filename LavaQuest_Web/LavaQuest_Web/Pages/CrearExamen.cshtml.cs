using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Numerics;
using LavaQuest_Web.Model;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Crmf;
using System.Runtime.ConstrainedExecution;

namespace LavaQuest_Web.Pages
{
    public class CrearExamenModel : PageModel
    {

        [BindProperty]
        public string NomExamen { get; set; }

        [BindProperty]
        public string Pregunta { get; set; }

        [BindProperty]
        public string Respuesta1 { get; set; }

        [BindProperty]
        public string Respuesta2 { get; set; }

        [BindProperty]
        public string Respuesta3 { get; set; }

        [BindProperty]
        public string Respuesta4 { get; set; }

        public IList<Pregunta> listaPreguntas { get; set; }
        private string idExam { get; set; }

        public static string idPreg;



        public void OnGet()
        {
            listaPreguntas = new List<Pregunta>();
        }

        public void OnPostPreguntaSub()
        {
            //Declarar el ID del examen que se creó
            int? id = HttpContext.Session.GetInt32("idExamen");


            //Checar cual debe ser la respuesta correcta
            string correcta = Request.Form["correcta"];
            int cor;

            if (correcta == "1")
            {
                cor = 1;
            }
            else if (correcta == "2")
            {
                cor = 2;
            }
            else if (correcta == "3")
            {
                cor = 3;
            }
            else
            {
                cor = 4;
            }


            //Checar si el usuario desea editar una pregunta existente o crear una nueva
            if (idPreg == "-1" || idPreg == null) 
            {

                // String de conexion a la base de datos
                string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

                //Abriendo conexion a la BD
                using var con = new MySqlConnection(cs);
                con.Open();

                //Creando una nueva linea de comando de MySQL
                using var cmnd = new MySqlCommand();
                cmnd.Connection = con;


                //Agregar la pregunta nueva a la BD
                cmnd.CommandText = "INSERT INTO preguntas(idPregunta, idExamen, pregunta, respuesta1, respuesta2, respuesta3, respuesta4, correcta) VALUES(NULL, @idExamen, @Pregunta, @Respuesta1, @Respuesta2, @Respuesta3, @Respuesta4, @cor)";
                cmnd.Parameters.AddWithValue("@Pregunta", Pregunta);
                cmnd.Parameters.AddWithValue("@Respuesta1", Respuesta1);
                cmnd.Parameters.AddWithValue("@Respuesta2", Respuesta2);
                cmnd.Parameters.AddWithValue("@Respuesta3", Respuesta3);
                cmnd.Parameters.AddWithValue("@Respuesta4", Respuesta4);
                cmnd.Parameters.AddWithValue("@cor", cor);
                cmnd.Parameters.AddWithValue("@idExamen", id);
                cmnd.ExecuteNonQuery();

                //Volver a cargar la lista de preguntas con las preguntas de la base de datos
                CargaPreguntas();


                //Hacer nulo los valores para hacer reset a las cajas de entrada de texto
                Pregunta = null;
                Respuesta1 = null;
                Respuesta2 = null;
                Respuesta3 = null;
                Respuesta4 = null;

                ModelState.Clear();

                CargaPreguntas();
            }

            //Se ejecuta cuando se selecciona una pregunta existente y se quiere editar
            else
            {
                //String de conexión a la base de datos
                string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

                //Abriendo la conexión
                MySqlConnection conexion = new MySqlConnection(cs);
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;

                //Query para actualizar la pregunta actual
                cmd.CommandText = "UPDATE preguntas SET pregunta = @Pregunta, respuesta1 = @Respuesta1, respuesta2 = @Respuesta2, respuesta3 = @Respuesta3, respuesta4 = @Respuesta4 , correcta = @cor WHERE idPregunta = " + idPreg;
                cmd.Parameters.AddWithValue("@Pregunta", Pregunta);
                cmd.Parameters.AddWithValue("@Respuesta1", Respuesta1);
                cmd.Parameters.AddWithValue("@Respuesta2", Respuesta2);
                cmd.Parameters.AddWithValue("@Respuesta3", Respuesta3);
                cmd.Parameters.AddWithValue("@Respuesta4", Respuesta4);
                cmd.Parameters.AddWithValue("@cor", cor);
                cmd.Parameters.AddWithValue("@idExamen", id);
                cmd.ExecuteNonQuery();

                //Volver a cargar la lista de preguntas con las preguntas de la base de datos
                CargaPreguntas();
            }


        }

        public void OnPostExamen(string idBoton)
        {

            //Recibe el valor de la pregunta a la cual se le acaba de dar click

            //Si se hace click al botón de nueva pregunta, se ejecuta
            if (idBoton == "0")
            {
                idPreg = "-1";

                Pregunta = null;
                Respuesta1 = null;
                Respuesta2 = null;
                Respuesta3 = null;
                Respuesta4 = null;

                ModelState.Clear();

                CargaPreguntas();

            }
            //Si se hace click en una pregunta existente, se ejecuta
            else
            {
                idPreg = idBoton;

                int? id = HttpContext.Session.GetInt32("idExamen");

                //Se cargan las preguntas de la base de datos
                CargaPreguntas();

                //String de conexión a la base de datos
                string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

                //Se crea una conexión nueva
                MySqlConnection conexion = new MySqlConnection(cs);
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;

                //Query para leer la pregunta de la base de datos con el ID especificado
                cmd.CommandText = "Select * from preguntas where idPregunta = " + idBoton;

                //Inicializar objeto pregunta y una lista de preguntas
                Pregunta preg = new Pregunta();
                listaPreguntas = new List<Pregunta>();

                //Asignar valor a las variables para que se desplieguen en las cajas de texto de la Front-End
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pregunta = reader["pregunta"].ToString();
                        Respuesta1 = reader["respuesta1"].ToString();
                        Respuesta2 = reader["respuesta2"].ToString();
                        Respuesta3 = reader["respuesta3"].ToString();
                        Respuesta4 = reader["respuesta4"].ToString();
                    }
                }

                CargaPreguntas();
            }


        }

        //Función para cargar las preguntas de la base de datos a la lista de preguntas para desplegarlas en la pagina
        private void CargaPreguntas()
        {
            int? id = HttpContext.Session.GetInt32("idExamen");

            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            MySqlConnection conexion = new MySqlConnection(cs);
            conexion.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;

            //Query para leer todos los renglones de la tabla preguntas
            cmd.CommandText = "Select * from preguntas where idExamen = " + id;

            //Inicializar objeto pregunta y una lista de preguntas
            Pregunta preg = new Pregunta();
            listaPreguntas = new List<Pregunta>();

            // Leer cada columna de la tabla y agregar el objeto a la lista
            // Esta lista se usara para desplegar las preguntas existentes en la pagina
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    preg = new Pregunta();
                    preg.idPregunta = Convert.ToInt32(reader["idPregunta"]);
                    preg.idExamen = Convert.ToInt32(reader["idExamen"]);
                    preg.pregunta = reader["pregunta"].ToString();
                    preg.respuesta1 = reader["respuesta1"].ToString();
                    preg.respuesta2 = reader["respuesta2"].ToString();
                    preg.respuesta3 = reader["respuesta3"].ToString();
                    preg.respuesta4 = reader["respuesta4"].ToString();
                    preg.correcta = Convert.ToInt32(reader["correcta"]);

                    listaPreguntas.Add(preg);
                }
            }
        }

        //Función para eliminar una pregunta del examen y de la base de datos
        public void OnPostEliminar()
        {

            int? id = HttpContext.Session.GetInt32("idExamen");

            //String de conexión a la base de datos
            string cs = @"server=localhost;userid=root;password=Madrid99.;database=lavaquestbd; Allow User Variables = True;";

            //Abriendo conexion a la BD
            using var con = new MySqlConnection(cs);
            con.Open();
            using var cmnd = new MySqlCommand();
            cmnd.Connection = con;

            //Query para eliminar de la base de datos la pregunta especificada
            cmnd.CommandText = "DELETE FROM preguntas WHERE idPregunta = " + idPreg;
            cmnd.ExecuteNonQuery();

            //Volver cargar la lista de preguntas con la base de datos actualizada
            CargaPreguntas();
        }
    }
}
