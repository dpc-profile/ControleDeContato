using System.Collections.Generic;

using ControleDeContatos.Models;

namespace ControleDeContatos.Services.Interfaces
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