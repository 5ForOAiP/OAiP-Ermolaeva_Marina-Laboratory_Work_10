using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace lab10OAiP.services
{
        public static class PasswordHasher
        {
            public static string HashPassword(string password)
            {
                var salt = RandomNumberGenerator.GetBytes(16);

                using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))
                {
                    Salt = salt,
                    Iterations = 3,        
                    MemorySize = 65536,    
                    DegreeOfParallelism = 1 
                };

                var hash = hasher.GetBytes(32);

                var combined = new byte[48];
                Buffer.BlockCopy(salt, 0, combined, 0, 16);
                Buffer.BlockCopy(hash, 0, combined, 16, 32);

                return Convert.ToBase64String(combined);
            }

            public static bool VerifyPassword(string password, string storedHash)
            {
                var combined = Convert.FromBase64String(storedHash);

                var salt = combined[0..16];

                var originalHash = combined[16..48];

                using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))
                {
                    Salt = salt,
                    Iterations = 3,
                    MemorySize = 65536,
                    DegreeOfParallelism = 1
                };
                var newHash = hasher.GetBytes(32);

                return CryptographicOperations.FixedTimeEquals(originalHash, newHash);
            }
        }
}

