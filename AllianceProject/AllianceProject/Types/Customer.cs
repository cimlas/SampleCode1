namespace AllianceProject.Types
{
    public class Customer : PersistenceBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        public Customer(string pFirstName, string pLastName, Address pAddress)
        {
            FirstName = pFirstName;
            LastName = pLastName;
            Address = pAddress;
            Id = new CustomerId();
        }

        public static Customer Find(Identifier pId)
        {
            return PersistenceBase.Find(pId as CustomerId);
        }

        public override bool Equals(object pOther)
        {
            var toCompareWith = pOther as Customer;
            if (toCompareWith == null)
                return false;
            return string.Equals(this.ToString(), toCompareWith.ToString());
        }

        protected bool Equals(Customer pOther)
        {
            return string.Equals(this.ToString(), pOther.ToString());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Address != null ? Address.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return FirstName + LastName + Address;
        }
    }
}
