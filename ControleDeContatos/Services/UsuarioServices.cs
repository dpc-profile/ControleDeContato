using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services.Interfaces;

namespace ControleDeContatos.Services
{
    public class UsuarioServices : IUSuarioServices
    {
        private readonly IUsuarioServices _usuarioRepository;

        public UsuarioServices(IUsuarioServices usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void AdicionarUsuario(UsuarioModel usuario)
        {
            try
            {
                usuario.DataCadastro = DateTime.Now;
                usuario.SetSenhaHash();
                _usuarioRepository.Adicionar(usuario);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public void ApagarUsuario(int id)
        {
            try
            {
                UsuarioModel usuarioDb = _usuarioRepository.ListarPorId(id);

                if (usuarioDb == null) throw new Exception("Usuário não existe");

                _usuarioRepository.Apagar(usuarioDb);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public void AtualizarUsuario(UsuarioModel usuario)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel BuscarUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel BuscarUsuarioPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel BuscarUsuarioPorLogin(string login)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioModel> BuscarUsuarios()
        {
            throw new NotImplementedException();
        }

        public void EditarUsuario(UsuarioModel usuario)
        {
            throw new NotImplementedException();
        }
    }
}