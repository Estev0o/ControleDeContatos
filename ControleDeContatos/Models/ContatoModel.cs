using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Digite o nome do contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o E-mail do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado nao e valido")]
        public string Email { get; set;}
        
        [Required(ErrorMessage = "Digite o celular do contato")]
        public string Celular { get; set;}

        public int? UsuarioID { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}
