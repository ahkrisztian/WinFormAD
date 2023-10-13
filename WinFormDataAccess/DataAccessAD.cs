using System.DirectoryServices;
using System.Runtime.Versioning;
using Microsoft.Extensions.Configuration;

namespace WinFormDataAccess;

[SupportedOSPlatform("windows")]
public class DataAccessAD : IDataAccessAD
{
    public string passwordAdmin { get; set; }
    public string serverIp {  get; set; }
    public string userName { get; set; }

    public DirectoryEntry ConnectToAD()
    {
        try
        {
            DirectoryEntry ldapConnection = new DirectoryEntry(serverIp, userName, passwordAdmin);

            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;


            if (ldapConnection is not null)
            {
                return ldapConnection;
            }

            throw new InvalidOperationException("Can not connect to DirectoryEntry");
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
