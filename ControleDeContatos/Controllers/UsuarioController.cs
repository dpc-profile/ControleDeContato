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
        private readonly IUsuarioServices _usuarioRepository;
        private readonly IUSuarioServices _usuarioServices;


        public UsuarioController(IUsuarioServices usuarioRepository, IUSuarioServices usuarioServices)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioServices = usuarioServices;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> allUsuarios = _usuarioRepository.BuscarTodos();

            return View(allUsuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepository.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepository.ListarPorId(id);
            return View(usuario);
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
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenha.Id,
                        Nome = usuarioSemSenha.Nome,
                        Email = usuarioSemSenha.Email,
                        Login = usuarioSemSenha.Login,
                        Perfil = usuarioSemSenha.Perfil
                    };

                    _usuarioServices.AtualizarUsuario(usuario);

                    //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizado o usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }



        }
    }
}
