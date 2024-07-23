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
    public class UserProfileAchievement // AKA User Portfolio
    {
        [DataMember]
        public Guid UserProfileAchievementId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Name of Achievement")]
        public string AchievementName { get; set; }
        [DataMember]
        public string Description { get; set; }//Most welcome to include links
        [DataMember]
        [Required]
        [Display(Name = "Achieved At")]
        public string AchievedAt { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Date Achieved")]
        public DateTime? AchievedOn { get; set; }
        [DataMember]
        public bool IsVerified { get; set; }
    }
}
