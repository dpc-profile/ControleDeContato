using System.Diagnostics.CodeAnalysis;

using ControleDeContatos.Filters;

using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [ExcludeFromCodeCoverage]
    [PaginaUsuarioLogadoAttribute]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}