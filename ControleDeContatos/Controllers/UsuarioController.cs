using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ControleDeContatos.Repository;
using ControleDeContatos.Models;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepository.Adicionar(usuario);

                    //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (System.Exception erro)
            {
                //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }


        }
    }
}
