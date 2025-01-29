using Microsoft.AspNetCore.Mvc;

namespace SistemaLivros.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
