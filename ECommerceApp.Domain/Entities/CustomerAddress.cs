namespace ECommerceApp.Domain.Entities
{
    public class CustomerAddressEntity
    {
        public CustomerAddressEntity(AddressEntity address, bool isDefault = false) {
            Address = address;
            IsDefault = isDefault;
        }

        public AddressEntity Address { get; }
        public bool IsDefault { get; private set; }

        public void SetAsDefault()
        {
            IsDefault = true;
        }
    }
}
