using System.Security.Cryptography;
using System.Text;

namespace ControleDeContatos.Services
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        {
            SHA1 hash = SHA1.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] array = encoding.GetBytes(valor);

            array = hash.ComputeHash(array);

            StringBuilder strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}