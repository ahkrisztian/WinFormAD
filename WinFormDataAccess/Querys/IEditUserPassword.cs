using System.DirectoryServices;

namespace WinFormDataAccess.Querys;

public interface IEditUserPassword
{
    string SetUserPasswordNextLogon(DirectoryEntry direntry, string queryusername, bool checkedCheckbox);
    string SetUserPasswordNeverExpires(DirectoryEntry direntry, string queryusername, bool checkedCheckbox);

    bool CheckPasswordNeverExpires(DirectoryEntry direntry, string queryusername);
    bool CheckPasswordMustBeChangeNextLogin(DirectoryEntry direntry, string queryusername);
}