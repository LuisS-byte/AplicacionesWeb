﻿namespace AplicacionesWeb.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Nacionalidad { get; set; }
        public virtual List<string> Libros { get; set; }
    }
}
