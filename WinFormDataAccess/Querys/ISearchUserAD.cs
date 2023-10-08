using System.DirectoryServices;

namespace WinFormDataAccess.Querys
{
    public interface ISearchUserAD
    {
        Task<string> QueryUserAD(DirectoryEntry direntry, string queryusername);
    }
}