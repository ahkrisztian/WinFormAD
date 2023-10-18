using System.DirectoryServices;

namespace WinFormDataAccess
{
    public interface IDataAccessAD
    {
        Task<DirectoryEntry> ConnectToAD(CancellationToken cancellationToken);

        void SetThePassword(string password, string server, string user);
        void DisconnectAD(DirectoryEntry ldapConnection);
    }
}