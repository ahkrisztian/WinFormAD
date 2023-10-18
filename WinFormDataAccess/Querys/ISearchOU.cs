using WindiwsFormAdModels.OUModel;

namespace WinFormDataAccess.Querys
{
    public interface ISearchOU
    {
        Task<List<OrganizationUnitModel>> SearchOrganizationalUnits(CancellationToken cancellationToken);

        Task<List<string>> SearchMembersOfOrganizationalUnits(OrganizationUnitModel oumodel, CancellationToken cancellationToken);
    }
}