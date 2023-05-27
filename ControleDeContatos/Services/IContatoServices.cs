using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;

namespace ControleDeContatos.Services
{
    public interface IContatoServices
    {
        public void CriarContato (ContatoModel contato);
        public void AtualizarContato (ContatoModel contato);
        public void ApagarContato (int id);
        public List<ContatoModel> BuscarContatos (int id);
        public ContatoModel BuscarContato (int id);

    }
}