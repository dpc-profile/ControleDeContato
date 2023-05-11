using System;

using ControleDeContatos.Models;

using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        // public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<ContatoModel> Contatos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // string stringconexao = "server=mysql_db;database=db;user=user;password=passUser";
            string stringconexao = "server=mysql_db;database=db_teste;user=user;password=passUser";
            optionsBuilder.UseMySql(stringconexao, ServerVersion.AutoDetect(stringconexao));
        }
    }
}
