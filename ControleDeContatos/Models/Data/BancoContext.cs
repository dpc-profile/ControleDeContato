using System;
using System.Linq;

using ControleDeContatos.Models;

using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        // public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<ContatoModel> Contatos { get; set; }

        // Detecta se está rodando pelo comando dotnet test
        public static readonly bool IsRunningFromNUnit = AppDomain
                .CurrentDomain.GetAssemblies()
                .Any(a => a.FullName.ToLowerInvariant().StartsWith("xunit"));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string stringconexao = "server=mysql_db;database=db;user=user;password=passUser";
            
            if (IsRunningFromNUnit)
            {
                stringconexao = "server=mysql_db;database=db_teste;user=user;password=passUser";

            }
            
            optionsBuilder.UseMySql(stringconexao, ServerVersion.AutoDetect(stringconexao));
        }
    }
}
