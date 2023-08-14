using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio,
            ISessao sessao,
            IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            //se o usuario estiver logado redirecionar para a tela Home

            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginmodel.Login);

                    if (usuario != null)
                    {
                        if (usuario.Senhavalid(loginmodel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Oops, usuario invalido, tente novamente";
                    }

                    TempData["MensagemErro"] = $"Oops, senha invalido, tente novamente";

                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops, nao foi possivel logar, tente novamento {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailPorLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha e: {novaSenha}";

                        bool emailEnviado = this._email.enviar(usuario.Email, "Sistema de Contatos - Nova Senha - by Vinicius", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = "Enviamos para seu e-mail cadastrado uma nova senha";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Oops, nao foi possivel enviar sua senha, tente novamente";

                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";

                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops, nao foi possivel redefinir sua senha, tente novamente {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
