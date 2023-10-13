using WindiwsFormAdModels.UserModels;

namespace WinFormDataAccess.Querys
{
    public interface ISearchUserAD
    {
        Task<UserAD> QueryUserAD(string queryusername);
    }
}