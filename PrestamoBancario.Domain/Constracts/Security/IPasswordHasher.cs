namespace PrestamoBancario.Domain.Constracts.Security
{
    internal interface IPasswordHasher
    {
        string Hash(string raw);
        bool Verify(string raw, string hash);
    }
}
