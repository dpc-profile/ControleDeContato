using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ControleDeContatos.Repository;
using ControleDeContatos.Models;
using ControleDeContatos.Filters;
using ControleDeContatos.Services.Interfaces;

namespace ControleDeContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUSuarioServices _usuarioServices;

        public UsuarioController(IUSuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        public IActionResult Index()
        {
            return View(_usuarioServices.BuscarUsuarios());
        }

        public IActionResult Editar(int id)
        {
            return View(_usuarioServices.BuscarUsuario(id));
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            return View(_usuarioServices.BuscarUsuario(id));
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                _usuarioServices.ApagarUsuario(id);

                TempData["MensagemSucesso"] = "Usuário apagado com sucesso";

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Criar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioServices.AdicionarUsuario(usuario);

                    //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (System.Exception erro)
            {
                //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioServices.AtualizarUsuario(usuarioSemSenha);

                    //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizado o usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
