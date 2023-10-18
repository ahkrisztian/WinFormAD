namespace WinFormDataAccess.Querys
{
    public interface ISearchOU
    {
        Task<List<string>> SearchOrganizationalUnits(CancellationToken cancellationToken);

        Task<List<string>> SearchMembersOfOrganizationalUnits(string ou, CancellationToken cancellationToken);
    }
}