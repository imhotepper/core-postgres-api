using Microsoft.EntityFrameworkCore;

namespace core_pg.Entities
{
    public class DbCtx: DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public DbCtx(DbContextOptions<DbCtx> options) : base(options)
        {
        }

       
    }
}