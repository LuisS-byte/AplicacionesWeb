﻿using AplicacionesWeb.Data;
using AplicacionesWeb.DTOs;
using AplicacionesWeb.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Lista()
        {
            List<Autor> Lista = await _appDbContext.autores.ToListAsync();
            return Ok(Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AutorPorId(int id)
        {

            //Autor autor = await _appDbContext.autores.Include(p).ToListAsync();
            var DTO = await _appDbContext.autores.Include(p => p.Id).FirstOrDefaultAsync(a => a.Id == id);



            var DTOList = new AutorDTO
            {
                Id = DTO.Id,
                Nombre = DTO.Nombre,
                Nacionalidad = DTO.Nacionalidad,
                Libro = DTO.Nombre
            };
            return Ok(DTOList);


        }
    }
}
