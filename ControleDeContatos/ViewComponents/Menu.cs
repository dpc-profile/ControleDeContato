using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using ControleDeContatos.Models;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeContatos.ViewComponents
{
    [ExcludeFromCodeCoverage]
    public class Menu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

            return View(usuario);
        }
    }
}