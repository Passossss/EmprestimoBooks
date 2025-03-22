using SistemaLivros.Models;

namespace SistemaLivros.SessaoService
{
    public interface ISessaoInterface
    {

        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void RemoverSessao();
       


    }
}
