﻿using WebApplication1.Models.BusinessOpportunitys;

namespace WebApplication1.Models
{
    public class UserBusinessOpportunity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int BusinessOpportunityId { get; set; }
        public BusinessOpportunity BusinessOpportunity { get; set; }
    }
}