using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a senha atual do usuario")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a senha nova do usuario")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme a senha nova do usuario")]
        [Compare("NovaSenha",ErrorMessage ="A senha nao confere com nova senha")]
        public string ConfirmarNovaSenha { set; get; }

    }
}
