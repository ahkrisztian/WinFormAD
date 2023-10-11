using System.DirectoryServices.AccountManagement;

namespace WinFormDataAccess
{
    public interface IPrincipialContextDataAccess
    {
        PrincipalContext principialContext(string password);
    }
}