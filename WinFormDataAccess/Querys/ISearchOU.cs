namespace WinFormDataAccess.Querys
{
    public interface ISearchOU
    {
        List<string> SearchOrganizationalUnits();

        List<string> SearchMembersOfOrganizationalUnits(string ou);
    }
}