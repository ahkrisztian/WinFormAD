using System.DirectoryServices;
using System.Threading;

namespace WinFormDataAccess.Querys;

public interface IEditUserPassword
{
    Task<string> SetUserPasswordNextLogon(string queryusername, bool checkedCheckbox, CancellationToken cancellationToken);
    Task<string> SetUserPasswordNeverExpires(string queryusername, bool checkedCheckbox, CancellationToken cancellationToken);

    string UserPasswordLastSetDateTime(DirectoryEntry user);
    Task<string> SetNewPassword(string username, string newPassword, CancellationToken cancellationToken);
    Task<bool> CheckPasswordNeverExpires(string queryusername, CancellationToken cancellationToken);
    Task<bool> CheckPasswordMustBeChangeNextLogin(string queryusername, CancellationToken cancellationToken);

}