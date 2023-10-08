using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormDataAccess.Querys
{
    public class SearchUserAD : ISearchUserAD
    {
        public async Task<string> QueryUserAD(DirectoryEntry direntry, string queryusername)
        {
            
            try
            {
                DirectorySearcher search = new DirectorySearcher(direntry);

                search.Filter = $"(samaccountname={queryusername})";

                SearchResult result = search.FindOne();

                if (result is not null)
                {
                    string displayName = result.Properties["displayName"][0].ToString();

                    return displayName;
                }
                else
                {
                    return "User does not exists";
                }
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }
    }
}
