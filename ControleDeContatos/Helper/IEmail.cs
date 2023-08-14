namespace ControleDeContatos.Helper
{
    public interface IEmail
    {
        bool enviar(string email, string assunto, string mensagem);
    }
}
