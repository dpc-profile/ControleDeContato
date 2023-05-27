using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using ControleDeContatos.Models;
using ControleDeContatos.Models.Data.Map;

using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    [ExcludeFromCodeCoverage]
    public class BancoContext : DbContext
    {
        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        // Detecta se está rodando pelo comando dotnet test
        public static readonly bool IsRunningFromXUnit = AppDomain
                .CurrentDomain.GetAssemblies()
                .Any(a => a.FullName.ToLowerInvariant().StartsWith("xunit"));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string stringconexao;
            string server = Environment.GetEnvironmentVariable("MYSQL_SERVER");
            string user = Environment.GetEnvironmentVariable("MYSQL_USER");            
            string pass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            
            if (IsRunningFromXUnit)
            {
                string db_teste = Environment.GetEnvironmentVariable("MYSQL_TESTE_DATABASE");
                string v = $"server={server};database={db_teste};user={user};password={pass}";
                stringconexao = v;

            } else {
                string db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
                string v = $"server={server};database={db};user={user};password={pass}";
                stringconexao = v;
                
            }
            
            optionsBuilder.UseMySql(stringconexao, ServerVersion.AutoDetect(stringconexao));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
