namespace WebApplication1.Models.DTO.CompanyDTO
{
    public class CompanyCreationDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int ResponsibleUserId { get; set; } // ID of the responsible user
        public string PostalCity { get; set; }
        public string OrganizationNumber { get; set; }
    }
}
