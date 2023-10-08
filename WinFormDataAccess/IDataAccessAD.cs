using System.DirectoryServices;

namespace WinFormDataAccess
{
    public interface IDataAccessAD
    {
        Task<DirectoryEntry> ConnectToAD(string path, string username, string password);
    }
}