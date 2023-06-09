using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ControleDeContatos.Models;
using ControleDeContatos.Services.Interfaces;
using ControleDeContatos.Filters;

namespace ControleDeContatos.Controllers
{
    [PaginaUsuarioLogadoAttribute]
    public class AlterarSenhaController : Controller
    {
        private readonly IAlterarSenhaServices _alterarSenhaServices;
        private readonly ISessao _sessao;

        public AlterarSenhaController(ISessao sessao,
                                      IAlterarSenhaServices alterarSenhaServices)
        {
            _sessao = sessao;
            _alterarSenhaServices = alterarSenhaServices;
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
                    _alterarSenhaServices.AlterarSenha(alterarSenha);

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
