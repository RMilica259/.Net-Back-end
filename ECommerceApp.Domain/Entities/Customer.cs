namespace ECommerceApp.Domain.Entities
{
    public class CustomerEntity
    {
        public CustomerEntity(string name, string email, HashSet<AddressEntity> addresses) 
        {
            Name = name;
            Email = email;
            Addresses = addresses;
        }

        public int Id { get; set; }
        public string Name { get; }
        public string Email { get; }
        public HashSet<AddressEntity> Addresses { get; }

        public void AddAddress(AddressEntity address)
        {
            if(address != null) 
                Addresses.Add(address);
        }
    }
}
