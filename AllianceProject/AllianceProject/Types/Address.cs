namespace AllianceProject.Types
{
    public class Address
    {
        public string Street { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Address(){}

        public Address(string pStreet, string pTown, string pState, string pZip)
        {
            Street = pStreet;
            Town = pTown;
            State = pState;
            Zip = pZip;
        }

        public override bool Equals(object pOther)
        {
            var toCompareWith = pOther as Address;
            if (toCompareWith == null)
                return false;
            return string.Equals(this.ToString(), toCompareWith.ToString());
        }

        protected bool Equals(Address pOther)
        {
            return string.Equals(this.ToString(), pOther.ToString());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Street != null ? Street.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Town != null ? Town.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (State != null ? State.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Zip != null ? Zip.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return Street + Town + State + Zip;
        }
    }
}
