namespace AplicacionesWeb.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateOnly AñoPublicación { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }

        public string Resumen { get; set; }
        public virtual Autor Autor { get; set; }
    }
}
