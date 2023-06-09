using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ControleDeContatos.Models;
using ControleDeContatos.Services.Interfaces;
using namesource.ControleDeContatos.Exceptions;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly ILoginServices _loginServices;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioServices usuarioServices,
                               ISessao sessao,
                               ILoginServices loginServices)
        {
            _usuarioServices = usuarioServices;
            _sessao = sessao;
            _loginServices = loginServices;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovoUsuario(UsuarioModel usuario)
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

                return View("Index");
            }
            catch (LoginJaCadastradoException erro)
            {
                TempData["MensagemErro"] = $"Erro ao criar usuario: {erro.Message}";
                return RedirectToAction("NovoUsuario");
            }
            catch (EmailJaCadastradoException erro)
            {
                TempData["MensagemErro"] = $"Erro ao criar usuario: {erro.Message}";
                return RedirectToAction("NovoUsuario");
            }
            catch (System.Exception erro)
            {
                //Cria uma variavel temporaria, para armazenar a mensagem pro index.cshtml
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuario, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("NovoUsuario");
            }
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _loginServices.FazerLogin(loginModel, _sessao);

                    return RedirectToAction("Index", "Home");
                }

                return View("Index");

            }
            catch (UsuarioInvalidoException erro)
            {
                TempData["MensagemErro"] = $"{erro.Message}";
                return RedirectToAction("Index");
            }
            catch (SenhaInvalidaException erro)
            {
                TempData["MensagemErro"] = $"{erro.Message}";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel fazer o login, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _loginServices.ValidaUsuarioCadastrado(redefinirSenha.Email, redefinirSenha.Login);

                    string novaSenha = usuario.GerarNovaSenha();
                    string mensagem = $"Sua nova senha é: {novaSenha}";

                    _loginServices.EnviarNovaSenha(usuario.Email, mensagem);

                    _usuarioServices.AtualizarUsuarioComSenha(usuario);

                    TempData["MensagemSucesso"] = "Foi enviado para o email informado uma nova senha.";

                    return RedirectToAction("Index", "Login");

                }

                return View("Index");
            }
            catch (EmailNaoEncontradoException erro)
            {
                TempData["MensagemErro"] = $"{erro.Message}";
                return View("RedefinirSenha");
            }
            catch (LoginNaoEncontradoException erro)
            {
                TempData["MensagemErro"] = $"{erro.Message}";
                return View("RedefinirSenha");
            }
            catch (FalhaAoEnviarEmailException erro)
            {
                TempData["MensagemErro"] = $"{erro.Message} A senha não foi resetada, tente novamente";
                return View("RedefinirSenha");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel redefinir sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }
    }
}
