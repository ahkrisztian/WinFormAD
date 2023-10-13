using System.DirectoryServices;
using System.Runtime.Versioning;
using WindiwsFormAdModels.UserModels;

namespace WinFormDataAccess.Querys;

[SupportedOSPlatform("windows")]
public class SearchUserAD : ISearchUserAD
{
    private readonly IDataAccessAD dataAccess;

    public SearchUserAD(IDataAccessAD dataAccess)
    {
        this.dataAccess = dataAccess;
    }
    public UserAD QueryUserAD(string queryusername)
    {      
        using(DirectoryEntry direntry = dataAccess.ConnectToAD())
        {
            try
            {
                DirectorySearcher search = new DirectorySearcher(direntry);

                search.Filter = $"(samaccountname={queryusername})";

                SearchResult? result = search.FindOne();

                if (result is not null)
                {
                    string? displayName = result.Properties["displayName"][0].ToString();

                    var user = new UserAD();

                    user.UserName = result.Properties["samaccountname"][0]?.ToString();
                    user.DisplayName = result.Properties["displayname"][0]?.ToString();
                    user.Email = result.Properties["mail"][0]?.ToString();
                    user.Address = $"{result.Properties["postalCode"][0]}" +
                        $" {result.Properties["streetAddress"][0]}" +
                        $" {result.Properties["l"][0]}" +
                        $" {result.Properties["st"][0]}" +
                        $" {result.Properties["c"][0]}";

                    user.PhoneNumber = result.Properties["telephonenumber"][0].ToString();
                    user.FirstName = result.Properties["givenname"][0].ToString();
                    user.LastName = result.Properties["sn"][0].ToString();

                    user.whenCreated = (DateTime)result.Properties["whenCreated"][0];

                    if (displayName is not null)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
                //throw new NullReferenceException("No result");
            }
        }
    }

    
}
