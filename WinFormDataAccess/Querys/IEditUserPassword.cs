using System.DirectoryServices;

namespace WinFormDataAccess.Querys;

public interface IEditUserPassword
{
    string SetUserPasswordNextLogon(string queryusername, bool checkedCheckbox);
    string SetUserPasswordNeverExpires(string queryusername, bool checkedCheckbox);

    string SetNewPassword(string username, string newPassword);
    bool CheckPasswordNeverExpires(string queryusername);
    bool CheckPasswordMustBeChangeNextLogin(string queryusername);
}