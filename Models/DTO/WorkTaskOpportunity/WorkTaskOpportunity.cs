using System;
using System.Collections.Generic;

namespace WebApplication1.Models.WorkTaskModel
{
    public class WorkTaskOpportunity
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public DateTime OpportunityDate { get; set; }
        public decimal OpportunityValue { get; set; }
        public bool IsClosed { get; set; } = false;

        public string PipelineLevel { get; set; } // Add PipelineLevel property
        public string SalesResponsible { get; set; }

        // Add this collection property to represent related UserWorkTaskOpportunity entities
        public ICollection<UserWorkTaskOpportunity> UserWorkTaskOpportunities { get; set; }

        // Add any other properties as needed
    }
}
