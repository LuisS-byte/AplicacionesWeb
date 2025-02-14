using AplicacionesWeb.Models;

namespace AplicacionesWeb.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Nacionalidad { get; set; }
        public string? Libro { get; set; }
    }
}
