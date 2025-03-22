using Microsoft.AspNetCore.Mvc;
using SistemaLivros.Dto;
using SistemaLivros.Services.LoginService;
using SistemaLivros.Services.SenhaService;
using SistemaLivros.SessaoService;

namespace SistemaLivros.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginInterface _loginInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public LoginController(ILoginInterface loginInterface, ISessaoInterface sessaoInterface)
        {
            _loginInterface = loginInterface;
            _sessaoInterface = sessaoInterface;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            _sessaoInterface.RemoverSessao();

            return RedirectToAction("Login");
        }


        public IActionResult Registrar()
        {
            return View();
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
            else
            {
                return View(userDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (!ModelState.IsValid)
            {
                var usuario = await _loginInterface.Login(usuarioLoginDto);
                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View("Login", usuarioLoginDto);
                }
            }
            else
            {
                return View(usuarioLoginDto);
            }

        }

    }
}

    
    

