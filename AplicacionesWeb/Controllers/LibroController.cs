using AplicacionesWeb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


    }
}
