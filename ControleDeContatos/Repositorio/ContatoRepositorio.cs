using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;

        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }

        public ContatoModel ListarPorID(int id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos(int usuarioID)
        {
            return _context.Contatos.Where(x => x.UsuarioID == usuarioID).ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
            {
            /*gravaar no banco de dados */
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return contato;
            }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel ContatoDB = ListarPorID(contato.Id);

            if (ContatoDB == null) throw new Exception("Houve um erro na atualizcao do contato");

            ContatoDB.Nome = contato.Nome;
            ContatoDB.Email = contato.Email;
            ContatoDB.Celular = contato.Celular;

            _context.Contatos.Update(ContatoDB);
            _context.SaveChanges();

            return ContatoDB;   
        }

        public bool Apagar(int id)
        {
            ContatoModel ContatoDB = ListarPorID(id);

            if (ContatoDB == null) throw new Exception("Houve um erro na delecao do contato");

            _context.Contatos.Remove(ContatoDB);
            _context.SaveChanges();

            return true;
        }
    }
}
