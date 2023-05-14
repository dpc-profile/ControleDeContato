using System;
using System.Linq;

using ControleDeContatos.Models;

using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        public DbSet<ContatoModel> Contatos { get; set; }

        // Detecta se está rodando pelo comando dotnet test
        public static readonly bool IsRunningFromXUnit = AppDomain
                .CurrentDomain.GetAssemblies()
                .Any(a => a.FullName.ToLowerInvariant().StartsWith("xunit"));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string stringconexao;
            string user = Environment.GetEnvironmentVariable("MYSQL_USER");
            string pass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            
            if (IsRunningFromXUnit)
            {
                string db_teste = Environment.GetEnvironmentVariable("MYSQL_TESTE_DATABASE");
                string v = $"server=mysql_db;database={db_teste};user={user};password={pass}";
                stringconexao = v;

            } else {
                string db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
                string v = $"server=mysql_db;database={db};user={user};password={pass}";
                stringconexao = v;
                
            }
            
            optionsBuilder.UseMySql(stringconexao, ServerVersion.AutoDetect(stringconexao));
        }
    }
}
