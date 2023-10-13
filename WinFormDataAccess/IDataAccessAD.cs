using System.DirectoryServices;

namespace WinFormDataAccess
{
    public interface IDataAccessAD
    {
        DirectoryEntry ConnectToAD();

        void SetThePassword(string password, string server, string user);
    }
}