using System.DirectoryServices;

namespace WinFormDataAccess
{
    public interface IDataAccessAD
    {
        DirectoryEntry ConnectToAD(string password);
    }
}