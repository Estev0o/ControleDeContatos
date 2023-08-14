using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorEmailPorLogin (string email, string login);
        UsuarioModel BuscarPorLogin (string login);

        UsuarioModel ListarPorID(int id);

        List<UsuarioModel> BuscarTodos();

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioModel usuario);

        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);

        bool Apagar(int id);
    }
}
