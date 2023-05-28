using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Services.Interfaces;
using ControleDeContatos.Filters;

namespace ControleDeContatos.Controllers
{
    [PaginaUsuarioLogado]
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepository usuarioRepository, ISessao sessao)
        {
            _usuarioRepository = usuarioRepository;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenha)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                alterarSenha.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {
                    _usuarioRepository.AlterarSenha(alterarSenha);
                    TempData["MensagemSucesso"] = "Senha atualizada com sucesso";
                    return View("Index", alterarSenha);
                }

                return View("Index", alterarSenha);
            }
            catch (System.Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizado o usuario, tente novamente, detalhe do erro: {e.Message}";
                return View("Index", alterarSenha);
            }
        }

    }
}
