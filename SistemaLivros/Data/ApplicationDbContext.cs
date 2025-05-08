using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SistemaLivros.Models;

namespace SistemaLivros.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
            public DbSet<EmprestimoModel> Emprestimos { get; set; }
            public DbSet<UsuarioModel> Usuarios { get; set; }
    }   
}
