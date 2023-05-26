using System.Diagnostics.CodeAnalysis;

using ControleDeContatos.Filters;

using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [ExcludeFromCodeCoverage]
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}