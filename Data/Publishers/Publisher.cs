namespace BlazorBookStore.Data.Publishers
{
    public class Publisher
    {


        public Publisher()
        {

        }
        public Publisher(string id, string name, string city, string state, string country)
        {
            Id = id;
            Name = name;
            City = city;
            State = state;
            Country = country;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}