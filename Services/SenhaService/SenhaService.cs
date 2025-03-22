using System.Security.Cryptography;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SistemaLivros.Services.SenhaService
{
    public class SenhaService : ISenhaInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificarSenha(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using(var hmac = new HMACSHA512(senhaSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);


                /*
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != senhaHash[i])
                    {
                        return false;
                    }
                }*/
            }
        }
    }
}
