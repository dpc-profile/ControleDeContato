namespace ControleDeContatos.Services.Interfaces
{
    public interface IEmail
    {
        void Enviar(string email, string assunto, string mensagem);

    }
}