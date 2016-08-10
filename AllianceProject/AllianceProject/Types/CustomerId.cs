namespace AllianceProject.Types
{
    public class CustomerId : Identifier
    {
        public static implicit operator string(CustomerId pCustomerId)
        {
            return pCustomerId?.Hash;
        }
    }
}
