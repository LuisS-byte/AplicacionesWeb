using AplicacionesWeb.Data;
using AplicacionesWeb.DTOs;
using AplicacionesWeb.Models;   
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AplicacionesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        public AutorController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> AutorLista()
        {
            var autores = await _appDbContext.autores
                .Select(a => new
                {
                    IdAutor = a.Id,
                    NombreAutor = a.Nombre,
                    NacionalidadAutor = a.Nacionalidad,
                    Libros = a.Libros.Select(l => new
                    {
                        IdLibro = l.Id,
                        Libro = l.Titulo,
                        AñoPublicación = l.AñoPublicación,
                        IdCategoria = l.CategoriaId,
                        Categoria = l.Categoria.Nombre,
                        Resumen = l.Resumen
                    }).ToList()
                }).ToListAsync();

            if (autores == null || !autores.Any())
            {
                return NotFound();
            }

            return Ok(autores);
        }




        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAutor([FromBody] AutorDTO AutorDTO)
        {
            var AutorDB = new Autor
            {
                Nombre = AutorDTO.Nombre,
                Nacionalidad = AutorDTO.Nacionalidad
            };

            await _appDbContext.autores.AddAsync(AutorDB);
            await _appDbContext.SaveChangesAsync();
            return Ok(AutorDB);
        }



        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> ActualizarAutor(int id, [FromBody] AutorDTO AutorModificar)
        {
            Autor? autorActual = await (from a in _appDbContext.autores
                                        where a.Id == id
                                        select a).Include(a => a.Libros).FirstOrDefaultAsync();

            if (autorActual == null)
            {
                return NotFound();
            }

            autorActual.Nombre = AutorModificar.Nombre;
            autorActual.Nacionalidad = AutorModificar.Nacionalidad;

            _appDbContext.Entry(autorActual).Property(a => a.Nombre).IsModified = true;
            _appDbContext.Entry(autorActual).Property(a => a.Nacionalidad).IsModified = true;

            await _appDbContext.SaveChangesAsync();

            return Ok(autorActual);
        }




        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> EliminarAutor(int id)
        {
            Autor? autor = await (from e in _appDbContext.autores
                                  where e.Id == id
                                  select e).FirstOrDefaultAsync();

            if (autor == null)
            {
                return NotFound();
            }

            _appDbContext.autores.Attach(autor);
            _appDbContext.autores.Remove(autor);
            await _appDbContext.SaveChangesAsync();

            return Ok(autor);
        }

    }
}
