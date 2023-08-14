using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailPorLogin(string email, string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper()&& x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel ListarPorID(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            /*gravaar no banco de dados */
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _context.Usuarios.Add(usuario);
            _context.SaveChanges(); 
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorID(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualizcao do usuario");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;   
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorID(alterarSenhaModel.Id);

            if (usuarioDB == null) throw new Exception("Ouve um erro na atualizacao dasenha usuario nao econtrado!");

            if (!usuarioDB.Senhavalid(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual nao confere!");

            if (usuarioDB.Senhavalid(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }


        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorID(id);

            if (usuarioDB == null) throw new Exception("Houve um erro na delecao do usuario");

            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();
            return true;
        }
    }
}
