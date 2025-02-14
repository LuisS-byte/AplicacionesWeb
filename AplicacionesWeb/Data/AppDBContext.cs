using Microsoft.EntityFrameworkCore;

namespace AplicacionesWeb.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions options): base(options)
        {
            
        }


    }
}
