using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Infraestructure.Security
{
    internal class PasswordHasher : IPasswordHasher
    {
        public string Hash(string raw) => BCrypt.Net.BCrypt.HashPassword(raw);
        public bool Verify(string raw, string hash) => BCrypt.Net.BCrypt.Verify(raw, hash);
    }
}
