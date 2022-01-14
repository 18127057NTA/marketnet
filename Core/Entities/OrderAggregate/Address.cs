namespace Core.Entities.OrderAggregate
{
    public class Address
    {
        public Address()
        {
        }

        public Address(
            string firstName, 
            string lastName, 
            string street,
            string ward,
            string district, 
            string city, 
            string province, 
            string zipCode
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            Ward = ward;
            District = district;
            City = city;
            Province = province;
            ZipCode = zipCode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Ward {get; set;}
        public string District {get; set;}
        public string City { get; set; }
        public string Province { get; set; }
        
        public string ZipCode { get; set; }
    }
}