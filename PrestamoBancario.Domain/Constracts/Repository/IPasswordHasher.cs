namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface IPasswordHasher
    {
        string Hash(string raw);
        bool Verify(string raw, string hash);
    }
}
