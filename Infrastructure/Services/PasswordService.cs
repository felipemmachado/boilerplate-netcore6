using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private static readonly int Iterations = 10000; //
        private static readonly int SaltSize = 16; // 128 bit
        private static readonly int KeySize = 32; // 256 bit

        public string Generate(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
               password,
               SaltSize,
               Iterations,
               HashAlgorithmName.SHA512);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{salt}.{key}";
        }

        public string GetAlphanumericCode(int qtdPosicao)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, qtdPosicao)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool Check(string chave, string password)
        {
            var parts = chave.Split('.', 2);

            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            using var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              Iterations,
              HashAlgorithmName.SHA512);
            var keyToCheck = algorithm.GetBytes(KeySize);

            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}
