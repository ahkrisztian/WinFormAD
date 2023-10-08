using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormDataAccess.Querys
{
    public class SearchUserAD
    {
        private readonly IDataAccessAD dataAccess;

        public SearchUserAD(IDataAccessAD dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<string> QueryUserAD(string path, string user, string password, string queryusername)
        {
            try
            {
                DirectoryEntry directoryEntry = await dataAccess.ConnectToAD(path, user, password);

                if (directoryEntry is not null)
                {
                    DirectorySearcher search = new DirectorySearcher(directoryEntry);

                    search.Filter = "(cn=" + queryusername + ")";

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

                return String.Empty;
            }
            catch (Exception ex)
            {

                throw new NullReferenceException(ex.Message);
            }
        }
    }
}
