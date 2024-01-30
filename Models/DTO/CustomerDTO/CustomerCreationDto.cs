namespace WebApplication1.Models.DTO.CustomerDTO
{
    public class CustomerCreationDto
    {
        public string CompanyName { get; set; }
        public string Number { get; set; }
        public string WebsiteLink { get; set; }
        public string PostalCode { get; set; }
        public string TypeOfCustomer { get; set; }
        public List<int> UserIds { get; set; }

    }
}
