using HARD.CORE.OBJ.Models;

namespace HARD.CORE.NEG.Interfaces
{
    public interface ICryptographerService
    {
        WebResultModel<string> CreateHash(string input);
        WebResultModel<bool> CompareHash(string input, string hash);
    }
}