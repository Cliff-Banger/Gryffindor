using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class UserProfileWork
    {
        [DataMember]
        public Guid UserProfileWorkId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [DataMember]
        [Required]
        public string Position { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Summary of responsibilities")]
        public string ResponsibilitiesSummary { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Date From")]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        [Display(Name = "Date To")]
        public DateTime? DateTo { get; set; }
    }
}
