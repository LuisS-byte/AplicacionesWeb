namespace AplicacionesWeb.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AñoPublicación { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }

        public string Resumen { get; set; }
    }
}
