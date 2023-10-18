using Serilog;
using System.DirectoryServices;

namespace WinFormDataAccess.Querys;

public class SearchOU : ISearchOU
{
    private readonly IDataAccessAD dataAccessAD;

    public SearchOU(IDataAccessAD dataAccessAD)
    {
        this.dataAccessAD = dataAccessAD;
    }

    public async Task<List<string>> SearchOrganizationalUnits(CancellationToken cancellationToken)
    {
        try
        {
            List<string> result = await Task.Run(async () =>
            {
                List<string> output = new List<string>();

                using (DirectoryEntry entry = await dataAccessAD.ConnectToAD(cancellationToken))
                {

                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = "(objectClass=organizationalUnit)";

                    // Perform the search.
                    SearchResultCollection results = searcher.FindAll();

                    // Iterate through the search results to list the OUs.
                    foreach (SearchResult result in results)
                    {
                        DirectoryEntry ou = result.GetDirectoryEntry();

                        output.Add(ou.Name);
                    }

                    if (output.Count > 0)
                    {
                        return output;
                    }
                    else
                    {
                        return new List<string> { };
                    }
                }
            });

            return result;
        }
        catch (NullReferenceException ex)
        {
            Log.Error(ex.Message);

            return new List<string> { };     

            throw new NullReferenceException($"Null Reference Exception: {ex.Message}");
        }
    }

    public async Task<List<string>> SearchMembersOfOrganizationalUnits(string ou, CancellationToken cancellationToken)
    {
        try
        {
            List<string> result =  await Task.Run(async () =>
            {
                List<string> output = new List<string>();

                using (DirectoryEntry entry = await dataAccessAD.ConnectToAD(cancellationToken))
                {
                    string ouPath = $"LDAP://192.168.178.75/{ou},DC=SERVER2022,DC=de";

                    entry.Path = ouPath;

                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = "(objectClass=user)";

                    // Perform the search.
                    SearchResultCollection results = searcher.FindAll();

                    // Iterate through the search results to list the OUs.
                    foreach (SearchResult result in results)
                    {
                        DirectoryEntry ouusers = result.GetDirectoryEntry();

                        output.Add(ouusers.Properties["samaccountname"].Value.ToString());
                    }

                    if (output.Count > 0)
                    {
                        return output;
                    }
                    else
                    {
                        return new List<string> { };
                    }
                }
            });

            return result;
        }
        catch (NullReferenceException ex)
        {
            Log.Error(ex.Message);

            return new List<string> { };

            throw new NullReferenceException($"Null Reference Exception: {ex.Message}");
        }
    }
}
