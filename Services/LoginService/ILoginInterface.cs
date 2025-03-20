using Microsoft.AspNetCore.Mvc;
using SistemaLivros.Dto;
using SistemaLivros.Models;

namespace SistemaLivros.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioRegisterDto);
    }
}
