using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using ControleDeContatos.Services.Interfaces;
using namesource.ControleDeContatos.Exceptions;

namespace ControleDeContatos.Services
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string userName = _configuration.GetValue<string>("SMTP:UserName");
                string nome = _configuration.GetValue<string>("SMTP:Nome");
                string host = _configuration.GetValue<string>("SMTP:Host");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                int porta = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage(){
                    From = new MailAddress(userName, nome)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(userName, senha);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                }
            }
            catch (System.Exception)
            {
                // Criar Logs????
                throw new FalhaAoEnviarEmailException("Não conseguimos enviar o email.");
            }
        }
    }
}