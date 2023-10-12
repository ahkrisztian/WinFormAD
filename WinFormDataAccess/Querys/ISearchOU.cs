namespace WinFormDataAccess.Querys
{
    public interface ISearchOU
    {
        List<string> SearchOrganizationalUnits(string password);

        List<string> SearchMembersOfOrganizationalUnits(string password, string ou);
    }
}