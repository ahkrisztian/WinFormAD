using System.DirectoryServices;
using System.Runtime.Versioning;
using WindiwsFormAdModels.UserModels;

namespace WinFormDataAccess.Querys;

[SupportedOSPlatform("windows")]
public class SearchUserAD : ISearchUserAD
{
    public string QueryUserAD(DirectoryEntry direntry, string queryusername)
    {      
        using(direntry)
        {
            try
            {
                DirectorySearcher search = new DirectorySearcher(direntry);

                search.Filter = $"(samaccountname={queryusername})";

                SearchResult? result = search.FindOne();

                if (result is not null)
                {
                    string? displayName = result.Properties["displayName"][0].ToString();

                    //var user = new UserAD();

                    //user.UserName = result.Properties["samaccountname"][0]?.ToString();
                    //user.DisplayName = result.Properties["displayname"][0]?.ToString();
                    //user.Email = result.Properties["mail"][0]?.ToString();

                    if (displayName is not null)
                    {
                        return displayName;
                    }
                    else
                    {
                        return String.Empty;
                    }
                }
                else
                {
                    return "User does not exists";
                }
            }
            catch (Exception ex)
            {
                return "No result";
                //throw new NullReferenceException("No result");
            }
        }
    }

    
}
