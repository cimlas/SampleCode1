namespace AllianceProject.Types
{
    public class Company : PersistenceBase
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Company(string pName, Address pAddress)
        {
            Name = pName;
            Address = pAddress;
            Id = new CompanyId();
        }

        public static Company Find(Identifier pId)
        {
            return PersistenceBase.Find(pId as CompanyId);
        }

        public override bool Equals(object pOther)
        {
            var toCompareWith = pOther as Company;
            if (toCompareWith == null)
                return false;
            return string.Equals(this.ToString(), toCompareWith.ToString());
        }

        protected bool Equals(Company other)
        {
            return string.Equals(this.ToString(), other.ToString());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (Address != null ? Address.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return Name + Address;
        }
    }
}
