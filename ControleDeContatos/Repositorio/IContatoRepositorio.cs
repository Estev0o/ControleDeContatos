using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorID(int id);

        List<ContatoModel> BuscarTodos(int usuarioID);

        ContatoModel Adicionar(ContatoModel contato);

        ContatoModel Atualizar(ContatoModel contato);

        bool Apagar(int id);
    }
}
