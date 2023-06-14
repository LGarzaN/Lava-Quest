using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaQuestDBAPI.Model
{
    public class Preguntas
    {
        public int idPregunta { set; get; }
        public int idExamen { set; get; }
        public string pregunta { set; get; }
        public string respuesta1 { set; get;}
        public string respuesta2 { set; get;}
        public string respuesta3 { set; get; }
        public string respuesta4 { set; get; }
        public int correcta { set; get; }

    }
}
