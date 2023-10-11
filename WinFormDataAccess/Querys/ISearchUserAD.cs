using System.DirectoryServices;

namespace WinFormDataAccess.Querys
{
    public interface ISearchUserAD
    {
        string QueryUserAD(DirectoryEntry direntry, string queryusername);
    }
}