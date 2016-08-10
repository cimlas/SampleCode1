using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AllianceProject.Types
{
    public class Identifier : IIdentifier
    {
        public string Hash { get; set; }
        public void FillIdentifier(string pData)
        {
            Hash = GetHashSha256(pData.ToLower().Replace(" ", ""));
        }

        public static implicit operator string(Identifier pIdentifier)
        {
            return pIdentifier?.Hash;
        }

        public override bool Equals(object pOther)
        {
            var toCompareWith = pOther as Identifier;
            return toCompareWith != null && string.Equals(Hash, toCompareWith.Hash);
        }

        protected bool Equals(Identifier pOther)
        {
            return string.Equals(Hash, pOther.Hash);
        }

        public override int GetHashCode()
        {
            return (Hash != null ? Hash.GetHashCode() : 0);
        }

        public static string GetHashSha256(string pText)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pText);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + String.Format("{0:x2}", x));
        }
    }
}
