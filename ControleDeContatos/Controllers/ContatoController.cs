using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;

        public IContatoRepositorio ContatoRepositorio => _contatoRepositorio;
        public ContatoController(IContatoRepositorio ContatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = ContatoRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = ContatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = ContatoRepositorio.ListarPorID(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = ContatoRepositorio.ListarPorID(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = ContatoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                }

                else
                {
                    TempData["MensagemErro"] = "Oops, nao foi possivel apagar seu contato!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops, nao foi possivel cadastrar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                contato.UsuarioID = usuarioLogado.Id;
                //_contatoRepositorio.Adicionar(contato);
                contato = ContatoRepositorio.Adicionar(contato);
                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                return RedirectToAction("Index");
                //}
                //return View(contato);

            }
            catch (Exception erro)
            {
                TempData["Mensagemerro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(ContatoModel contato)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioID = usuarioLogado.Id;

                    contato = ContatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato Cadastrado com sucesso";
                    return RedirectToAction("Index");
               // 
               //return View("Editar", contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Oops, nao foi possivel atualizar seu contato, tente novamente, detalhe do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }
     
        }
    }

}
