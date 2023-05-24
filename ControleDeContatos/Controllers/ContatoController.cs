using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using ControleDeContatos.Filters;
using ControleDeContatos.Helper;

namespace ControleDeContatos.Controllers
{
    [PaginaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly ISessao _sessao;

        public ContatoController(IContatoRepository contatoRepository, ISessao sessao)
        {
            _contatoRepository = contatoRepository;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            List<ContatoModel> allContatos = _contatoRepository.BuscarTodos(usuarioLogado.Id);

            return View(allContatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepository.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepository.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool isApagado = _contatoRepository.Apagar(id);

                if (isApagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não conseguimos apagar o contato";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepository.Adicionar(contato);

                    //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (System.Exception erro)
            {
                //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            if (ModelState.IsValid)
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                contato.UsuarioId = usuarioLogado.Id;

                var respostaAtualizar = _contatoRepository.Atualizar(contato);

                if (respostaAtualizar == null)
                {
                    //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                    TempData["MensagemErro"] = $"Ops, não conseguimos atualizado o contato, tente novamente.";
                    return RedirectToAction("Index");
                }

                //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                TempData["MensagemSucesso"] = "Contato atualizado com sucesso";
                return RedirectToAction("Index");
            }

            return View("Editar", contato);
        }

    }
}
