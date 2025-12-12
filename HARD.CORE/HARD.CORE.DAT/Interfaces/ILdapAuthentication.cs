namespace HARD.CORE.DAT.Interfaces
{
    public interface ILdapAuthentication
    {
        bool IsAutheticated(string domain, string username, string pwd);
    }
}