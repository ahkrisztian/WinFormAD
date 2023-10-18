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

    public async Task<DirectoryEntry> ConnectToAD(CancellationToken cancellationToken)
    {
        try
        {
            DirectoryEntry ldapConnection = new DirectoryEntry(serverIp, userName, passwordAdmin);
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(ldapConnection);

        }
        catch (DirectoryServicesCOMException ex)
        {
            Log.Warning("Error Connect to AD {message}", ex.Message);
            throw new InvalidOperationException();
        }
        catch(TaskCanceledException) { throw new TaskCanceledException(); }

    }

    public void DisconnectAD(DirectoryEntry ldapConnection)
    {
        if (ldapConnection != null)
        {
            ldapConnection.Dispose();
        }
    }
    public void SetThePassword(string password, string server, string user)
    {
        passwordAdmin = password;
        serverIp = server;
        userName = user;
    }
}
