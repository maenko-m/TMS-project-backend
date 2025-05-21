using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Utilities
{
    public static class PasswordHasher
    {
        private const int SALTSIZE = 16; 
        private const int KEYSIZE = 32;
        private const int ITERATIONS = 100_000;

        public static string Hash(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SALTSIZE];
            rng.GetBytes(salt);

            var key = new Rfc2898DeriveBytes(password, salt, ITERATIONS, HashAlgorithmName.SHA256);
            var hash = key.GetBytes(KEYSIZE);

            // Возвращаем в формате Base64: {iterations}.{salt}.{hash}
            return $"{ITERATIONS}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.', 3);
            if (parts.Length != 3)
                return false;

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var hash = Convert.FromBase64String(parts[2]);

            var key = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var hashToCompare = key.GetBytes(KEYSIZE);

            return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
        }
    }
}
