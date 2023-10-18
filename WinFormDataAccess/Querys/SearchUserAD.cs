using Serilog;
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
    public async Task<UserAD> QueryUserAD(string queryusername, CancellationToken cancellationToken)
    {      
        using(DirectoryEntry direntry = await dataAccess.ConnectToAD(cancellationToken))
        {
            try
            {
                DirectorySearcher search = new DirectorySearcher(direntry);

                search.Asynchronous = true;

                search.Filter = $"(samaccountname={queryusername})";

                SearchResult? result =  search.FindOne();

                if (result is not null)
                {
                    DirectoryEntry userDirEntry = result.GetDirectoryEntry();

                    IEditUserPassword editUserPassword = new EditUserPassword(dataAccess);

                    string pwdate = editUserPassword.UserPasswordLastSetDateTime(userDirEntry);

                    var user = new UserAD();

                    user.UserName = result.Properties["samaccountname"][0] != null ? result.Properties["samaccountname"][0]?.ToString() : "";
                    user.DisplayName = result.Properties["displayname"][0] != null ? result.Properties["displayname"][0]?.ToString() : "";

                    try
                    {
                        user.Email = result.Properties["mail"][0]?.ToString();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        user.Email = "";
                    }

                    try
                    {
                        user.Address = $"{result.Properties["postalCode"][0]}" +

                        $" {result.Properties["streetAddress"][0]}" +
                        $" {result.Properties["l"][0]}" +
                        $" {result.Properties["st"][0]}" +
                        $" {result.Properties["c"][0]}";
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        user.Address = "";
                    }

                    try
                    {
                        user.PhoneNumber = result.Properties["telephonenumber"][0].ToString();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        user.PhoneNumber = "";
                    }

                    try
                    {
                        user.FirstName = result.Properties["givenname"][0].ToString();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        user.FirstName = "";
                    }

                    try
                    {
                        user.LastName = result.Properties["sn"][0].ToString();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        user.LastName = "";
                    }

                    try
                    {
                        user.whenCreated = (DateTime)result.Properties["whenCreated"][0];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        user.whenCreated = DateTime.MinValue;
                    }


                    user.PassWordLastChanged = pwdate;

                    if (user.DisplayName is not null)
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
            catch (NullReferenceException ex)
            {
                Log.Error(ex.Message);

                return null;
                
                throw new NullReferenceException($"Null Reference Exception: {ex.Message}");
            }
        }
    }

    
}
