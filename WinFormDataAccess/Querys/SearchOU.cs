using Serilog;
using System.DirectoryServices;
using WindiwsFormAdModels.OUModel;

namespace WinFormDataAccess.Querys;

public class SearchOU : ISearchOU
{
    private readonly IDataAccessAD dataAccessAD;

    public SearchOU(IDataAccessAD dataAccessAD)
    {
        this.dataAccessAD = dataAccessAD;
    }

    public async Task<List<OrganizationUnitModel>> SearchOrganizationalUnits(CancellationToken cancellationToken)
    {
        List<OrganizationUnitModel> output = new List<OrganizationUnitModel>();

        try
        {
            await Task.Run(async () =>
            {              
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

                        OrganizationUnitModel organizationUnitModel = new OrganizationUnitModel();

                        organizationUnitModel.OUName = ou.Name;

                        organizationUnitModel.OUAddress = ou.Path;

                        output.Add(organizationUnitModel);
                    }
                }
            });

            return output;
        }
        catch (NullReferenceException ex)
        {
            Log.Error(ex.Message);

            return new List<OrganizationUnitModel> { };     

            throw new NullReferenceException($"Null Reference Exception: {ex.Message}");
        }
    }

    public async Task<List<string>> SearchMembersOfOrganizationalUnits(OrganizationUnitModel oumodel, CancellationToken cancellationToken)
    {
        List<string> output = new List<string>();

        try
        {
            await Task.Run(async () =>
            {
               
                using (DirectoryEntry entry = await dataAccessAD.ConnectToAD(cancellationToken))
                {
                    entry.Path = oumodel.OUAddress;

                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = "(objectClass=user)";


                    SearchResultCollection results = searcher.FindAll();


                    foreach (SearchResult result in results)
                    {
                        DirectoryEntry ouusers = result.GetDirectoryEntry();

                        output.Add(ouusers.Properties["samaccountname"].Value.ToString());
                    }

                }
            });

            return output;
        }
        catch (NullReferenceException ex)
        {
            Log.Error(ex.Message);

            return new List<string> { };

            throw new NullReferenceException($"Null Reference Exception: {ex.Message}");
        }
    }
}
