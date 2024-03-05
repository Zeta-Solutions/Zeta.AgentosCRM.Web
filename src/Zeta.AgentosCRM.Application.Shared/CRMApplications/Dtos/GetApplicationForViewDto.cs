using System;

namespace Zeta.AgentosCRM.CRMApplications.Dtos
{
    public class GetApplicationForViewDto
    {
        public ApplicationDto Application { get; set; }

        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientEmail { get; set; }

        public string WorkflowName { get; set; }

        public string PartnerPartnerName { get; set; }

        public string ProductName { get; set; }
        
        public string BranchName { get; set; }
        public string ApplicationName { get; set; }
        public string UserName { get; set; }
        public string OfficeName { get; set; }

		public bool IsCurrent { get; set; } 
		public bool IsActive { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime? DateofBirth { get; set; }
        public string Email { get; set; }
        public string ClientAssigneeName { get; set; }
        public long? AssigneeId { get; set; }
    }
}