using AplicacionesWeb.Models;
using System.Text.Json.Serialization;

public class Autor
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Nacionalidad { get; set; }

    [JsonIgnore] 
    public ICollection<Libro> Libros { get; set; }
}
