using AplicacionesWeb.Data;
using AplicacionesWeb.DTOs;
using AplicacionesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;

        public LibroController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerLibros()
        {
            var libros = await (from l in _appDbContext.libros
                                join a in _appDbContext.autores on l.AutorId equals a.Id
                                join c in _appDbContext.categorias on l.CategoriaId equals c.Id
                                select new
                                {
                                    IdLibro = l.Id,
                                    Titulo = l.Titulo,
                                    AñoPublicación = l.AñoPublicación,
                                    IdAutor = l.AutorId,
                                    Autor = new
                                    {
                                        IdAutor = a.Id,
                                        NombreAutor = a.Nombre,
                                        NacionalidadAutor = a.Nacionalidad
                                    },
                                    IdCategoria = l.CategoriaId,
                                    Categoria = new
                                    {
                                        IdCategoria = c.Id,
                                        NombreCategoria = c.Nombre
                                    },
                                    Resumen = l.Resumen
                                }).ToListAsync();

            if (libros == null || !libros.Any())
            {
                return NotFound("No hay libros registrados.");
            }

            return Ok(libros);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerLibroPorId(int id)
        {
            var libro = await (from l in _appDbContext.libros
                               join a in _appDbContext.autores on l.AutorId equals a.Id
                               join c in _appDbContext.categorias on l.CategoriaId equals c.Id
                               where l.Id == id
                               select new
                               {
                                   IdLibro = l.Id,
                                   Titulo = l.Titulo,
                                   AñoPublicación = l.AñoPublicación,
                                   IdAutor = l.AutorId,
                                   Autor = new
                                   {
                                       IdAutor = a.Id,
                                       NombreAutor = a.Nombre,
                                       NacionalidadAutor = a.Nacionalidad
                                   },
                                   IdCategoria = l.CategoriaId,
                                   Categoria = new
                                   {
                                       IdCategoria = c.Id,
                                       NombreCategoria = c.Nombre
                                   },
                                   Resumen = l.Resumen
                               }).FirstOrDefaultAsync();

            if (libro == null)
            {
                return NotFound($"No se encontró un libro con ID {id}.");
            }

            return Ok(libro);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AgregarLibro([FromBody] LibroDTO libroDTO)
        {
            bool autorExiste = await (from a in _appDbContext.autores where a.Id == libroDTO.AutorId select a).AnyAsync();
            bool categoriaExiste = await (from c in _appDbContext.categorias where c.Id == libroDTO.CategoriaId select c).AnyAsync();

            if (!autorExiste)
            {
                return NotFound($"No se encontró un autor con ID {libroDTO.AutorId}.");
            }

            if (!categoriaExiste)
            {
                return NotFound($"No se encontró una categoría con ID {libroDTO.CategoriaId}.");
            }

            var nuevoLibro = new Libro
            {
                Titulo = libroDTO.Titulo,
                AñoPublicación = libroDTO.AñoPublicación,
                AutorId = libroDTO.AutorId,
                CategoriaId = libroDTO.CategoriaId,
                Resumen = libroDTO.Resumen
            };

            await _appDbContext.libros.AddAsync(nuevoLibro);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerLibroPorId), new { id = nuevoLibro.Id }, nuevoLibro);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> ActualizarLibro(int id, [FromBody] LibroDTO libroDTO)
        {
            var libroExistente = await (from l in _appDbContext.libros where l.Id == id select l).FirstOrDefaultAsync();

            if (libroExistente == null)
            {
                return NotFound("El libro no fue encontrado.");
            }

            bool autorExiste = await (from a in _appDbContext.autores where a.Id == libroDTO.AutorId select a).AnyAsync();
            bool categoriaExiste = await (from c in _appDbContext.categorias where c.Id == libroDTO.CategoriaId select c).AnyAsync();

            if (!autorExiste || !categoriaExiste)
            {
                return BadRequest("El Autor o la Categoría no existen.");
            }

            libroExistente.Titulo = libroDTO.Titulo;
            libroExistente.AñoPublicación = libroDTO.AñoPublicación;
            libroExistente.AutorId = libroDTO.AutorId;
            libroExistente.CategoriaId = libroDTO.CategoriaId;
            libroExistente.Resumen = libroDTO.Resumen;

            _appDbContext.libros.Update(libroExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok(libroExistente);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> EliminarLibro(int id)
        {
            var libro = await (from l in _appDbContext.libros where l.Id == id select l).FirstOrDefaultAsync();

            if (libro == null)
            {
                return NotFound();
            }

            _appDbContext.libros.Remove(libro);
            await _appDbContext.SaveChangesAsync();
            return Ok(libro);
        }
    }
}
