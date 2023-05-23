using ControleDeContatos.Filters;

using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaUsuarioLogado]
    public class Restrito : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}