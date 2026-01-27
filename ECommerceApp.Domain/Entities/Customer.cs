namespace ECommerceApp.Domain.Entities
{
    public class CustomerEntity
    {
        public CustomerEntity(string name, string email) 
        {
            Name = name;
            Email = email;
            Addresses = new List<AddressEntity>();
        }

        public string Name { get; }
        public string Email { get; }
        public List<AddressEntity> Addresses { get; }

        public void AddAddress(AddressEntity address)
        {
            if(address != null) 
                Addresses.Add(address);
        }
    }
}
