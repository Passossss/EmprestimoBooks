using SistemaLivros.Models;

namespace SistemaLivros.Services.SessaoService
{
    public interface ISessaoInterface
    {

        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void RemoverSessao();



    }
}
