namespace LavaQuest_Web.Model
{
    public class Pregunta
    {
        public int idPregunta { get; set; }
        public int idExamen { get; set; }
        public string pregunta { get; set; }
        public string respuesta1 { get; set; }
        public string respuesta2 { get; set; }
        public string respuesta3 { get; set; }
        public string respuesta4 { get; set; }
        public int correcta { get; set; }
    }
}
