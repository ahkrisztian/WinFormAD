using Serilog;
using System.DirectoryServices;
using System.Runtime.Versioning;

namespace WinFormDataAccess;

[SupportedOSPlatform("windows")]
public class DataAccessAD : IDataAccessAD
{
    public string? passwordAdmin { get; set; }
    public string? serverIp {  get; set; }
    public string? userName { get; set; }

    public async Task<DirectoryEntry> ConnectToAD()
    {
        try
        {
            return await Task.Run(() => 
            {
                DirectoryEntry ldapConnection = new DirectoryEntry(serverIp, userName, passwordAdmin);

                ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

                return ldapConnection;
            });

                   
        }
        catch (Exception ex)
        {
            Log.Warning("Error Connect to AD {message}", ex.Message);
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
