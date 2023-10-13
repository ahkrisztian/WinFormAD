namespace WinFormDataAccess.Querys
{
    public interface ISearchOU
    {
        Task<List<string>> SearchOrganizationalUnits();

        Task<List<string>> SearchMembersOfOrganizationalUnits(string ou);
    }
}