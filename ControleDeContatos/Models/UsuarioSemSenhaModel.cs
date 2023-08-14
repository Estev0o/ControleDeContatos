using ControleDeContatos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do Usuario")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o login do Usuario")]
        public string Login { get; set;}
        [Required(ErrorMessage = "Digite o e-mail do Usuario")]
        public string Email { get; set;}
        [Required(ErrorMessage = "Informe o perfil do Usuario")]
        public PerfilEnum? Perfil { get; set;}


    }
}
