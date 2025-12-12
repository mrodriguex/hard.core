// HARD.CORE.NEG/Interfaces/ICryptographer.cs
namespace HARD.CORE.NEG.Interfaces
{
    public interface ICryptographer
    {
        string CreateHash(string algorithmName, string plainText);
        bool CompareHash(string algorithmName, string plainText, string hash);
    }
}