using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Job_Applications")]
   public class ApplicantJobApplicationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Applicant { get; set; }

        public Guid Job { get; set; }

        [Column("Application_Date")]
        public DateTime ApplicationDate { get; set; }

        [Timestamp]
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }

        public virtual CompanyJobPoco CompanyJob { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
