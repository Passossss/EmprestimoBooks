using Microsoft.AspNetCore.Mvc;
using SistemaLivros.Dto;
using SistemaLivros.Services.LoginService;
using SistemaLivros.Services.SenhaService;

namespace SistemaLivros.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginInterface _loginInterface;

        public LoginController(ILoginInterface loginInterface)
        {
            _loginInterface = loginInterface;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(UsuarioRegisterDto userDto)
    {
        if (ModelState.IsValid)
        {
            var usuario = await _loginInterface.RegistrarUsuario(userDto);
            if (usuario.Status)
            {
                TempData["MensagemSucesso"] = usuario.Mensagem;
            }
            else
            {
                TempData["MensagemErro"] = usuario.Mensagem;
                return View(userDto);
            }

            return RedirectToAction("Index");
        }
    }

    
    

