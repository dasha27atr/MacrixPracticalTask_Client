namespace MacrixPracticalTask_Client.Models.DTO
{
    public class PersonForCreationDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string streetName { get; set; }
        public int houseNumber { get; set; }
        public int? apartmentNumber { get; set; }
        public int postalCode { get; set; }
        public string town { get; set; }
        public string phoneNumber { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
