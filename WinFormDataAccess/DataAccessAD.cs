using System.DirectoryServices;
using System.Runtime.Versioning;

namespace WinFormDataAccess;

[SupportedOSPlatform("windows")]
public class DataAccessAD : IDataAccessAD
{
    public string passwordAdmin { get; set; }
    public string serverIp {  get; set; }
    public string userName { get; set; }

    public async Task<DirectoryEntry> ConnectToAD()
    {
        try
        {
            DirectoryEntry entry = await Task.Run(() =>
            {
                DirectoryEntry ldapConnection = new DirectoryEntry(serverIp, userName, passwordAdmin);

                ldapConnection.AuthenticationType = AuthenticationTypes.Secure;


                if (ldapConnection is not null)
                {
                    return ldapConnection;
                }

                throw new InvalidOperationException("Can not connect to DirectoryEntry");
            });

            return entry;
        }
        catch (Exception)
        {
            throw new InvalidOperationException();
        }
    }

    public void SetThePassword(string password, string server, string user)
    {
        passwordAdmin = password;
        serverIp = server;
        userName = user;
    }
}
