namespace ECommerceApp.Domain.Entities
{
    public class CustomerAddressEntity
    {
        public CustomerAddressEntity(
            string city,
            string street,
            string houseNumber,
            string zipCode,
            bool isDefault = false)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
            IsDefault = isDefault;
        }

        public int Id { get; set; }
        public string City { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string ZipCode { get; }
        public bool IsDefault { get; private set; }

        public void SetAsDefault()
        {
            IsDefault = true;
        }
    }
}
