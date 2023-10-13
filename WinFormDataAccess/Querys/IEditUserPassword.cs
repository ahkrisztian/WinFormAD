using System.DirectoryServices;

namespace WinFormDataAccess.Querys;

public interface IEditUserPassword
{
    Task<string> SetUserPasswordNextLogon(string queryusername, bool checkedCheckbox);
    Task<string> SetUserPasswordNeverExpires(string queryusername, bool checkedCheckbox);

    Task<string> SetNewPassword(string username, string newPassword);
    Task<bool> CheckPasswordNeverExpires(string queryusername);
    Task<bool> CheckPasswordMustBeChangeNextLogin(string queryusername);
}