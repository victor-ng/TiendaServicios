using Api.Libro.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        protected ContextoLibreria() { }

        public ContextoLibreria (DbContextOptions<ContextoLibreria> options) : base(options)
        {
        }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
