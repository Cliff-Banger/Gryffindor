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
    public class UserProfileSkill
    {
        [DataMember]
        public Guid UserProfileSkillId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Name of skill")]
        public string SkillName { get; set; }
        [DataMember]
        [Required]
        public int Rating { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Date Gained")]
        public DateTime? DateGained { get; set; }
        [DataMember]
        [Display(Name = "Last Practiced")]
        public DateTime? LastPracticed { get; set; }
    }
}
