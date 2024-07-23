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
    public class UserProfileEducation
    {
        [DataMember]
        public Guid UserProfileEducationId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        [Required]
        public string SchoolName { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Study Fields")]
        public string StudyFields { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Date From")]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Date From")]
        public DateTime? DateTo { get; set; }
    }
}
