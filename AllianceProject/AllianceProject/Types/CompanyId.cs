namespace AllianceProject.Types
{
    public class CompanyId : Identifier
    {
        public static implicit operator string(CompanyId pCompanyId)
        {
            return pCompanyId?.Hash;
        }
    }
}
