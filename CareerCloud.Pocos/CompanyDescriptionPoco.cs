using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Company { get; set; }

        public String LanguageId { get; set; }

        [Column("Company_Name")]
        public String CompanyName { get; set; }

        [Column("Company_Description")]
        public String CompanyDescription { get; set; }

        [Timestamp]
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }

        public virtual CompanyProfilePoco CompanyProfiles { get; set; }

        public virtual SystemLanguageCodePoco SystemLanguageCode { get; set; }
    }
}
