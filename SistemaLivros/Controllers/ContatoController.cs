using Microsoft.AspNetCore.Mvc;

namespace SistemaLivros.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
