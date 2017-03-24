using System;

namespace DataDumper.NorthWind
{
    public class AddressDetails : IEquatable<AddressDetails>
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public bool Equals(AddressDetails other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Address, other.Address)
                   && Equals(City, other.City)
                   && Equals(Region, other.Region)
                   && Equals(PostalCode, other.PostalCode)
                   && Equals(Country, other.Country);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AddressDetails)) return false;
            return Equals((AddressDetails) obj);
        }

        public override int GetHashCode()
        {
            var result = string.IsNullOrEmpty(Address) ? 0 : Address.GetHashCode();
            result ^= string.IsNullOrEmpty(City) ? 0 : City.GetHashCode();
            result ^= string.IsNullOrEmpty(Region) ? 0 : Region.GetHashCode();
            result ^= string.IsNullOrEmpty(PostalCode) ? 0 : PostalCode.GetHashCode();
            result ^= string.IsNullOrEmpty(Country) ? 0 : Country.GetHashCode();
            return result;
        }

        public static bool operator ==(AddressDetails left, AddressDetails right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AddressDetails left, AddressDetails right)
        {
            return !Equals(left, right);
        }
    }
}