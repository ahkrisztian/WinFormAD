using System.DirectoryServices;

namespace WinFormDataAccess.Querys;

public class SearchOU : ISearchOU
{
    private readonly IDataAccessAD dataAccessAD;

    public SearchOU(IDataAccessAD dataAccessAD)
    {
        this.dataAccessAD = dataAccessAD;
    }

    public List<string> SearchOrganizationalUnits()
    {
        try
        {
            List<string> output = new List<string>();

            using (DirectoryEntry entry = dataAccessAD.ConnectToAD())
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
        }
        catch (Exception)
        {
            return new List<string> { };
        }
    }

    public List<string> SearchMembersOfOrganizationalUnits(string ou)
    {
        try
        {
            List<string> output = new List<string>();

            using (DirectoryEntry entry = dataAccessAD.ConnectToAD())
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
        }
        catch (Exception)
        {
            return new List<string> { };
        }
    }
}
