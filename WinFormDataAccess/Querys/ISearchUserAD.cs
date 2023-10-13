using WindiwsFormAdModels.UserModels;

namespace WinFormDataAccess.Querys
{
    public interface ISearchUserAD
    {
        UserAD QueryUserAD(string queryusername);
    }
}