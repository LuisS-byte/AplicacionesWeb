namespace AplicacionesWeb.Models
{
    public class categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Libro> Libros { get; set; }
    }
}
