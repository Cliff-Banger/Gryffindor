using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class UserProfile
    {
        [DataMember]
        public Guid UserProfileId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        [Display(Name = "Name")]
        public string FirstName { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Avatar { get; set; }
        [DataMember]
        public string Field { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        [Display(Name = "Contact Email")]
        public string PreferredEmail { get; set; }
        [DataMember]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Bio { get; set; }
        [DataMember]
        [Display(Name = "Home Town")]
        public string HomeTown { get; set; }
        [DataMember]
        [Display(Name = "Preferred Profession")]
        public string PreferredProfession { get; set; }
        [DataMember]
        [Display (Name = "Preferred Work Area")]
        public string PreferredWorkArea { get; set; }
        [DataMember]
        [Display(Name = "Main Interest")]
        public int MainInterest { get; set; }
        [DataMember]
        public int ResumeTemplate { get; set; }
        [DataMember]
        public bool IsVerified { get; set; }
        [DataMember]
        public bool IsPrivate { get; set; }
        [DataMember]
        public string FullName { get; set; }
    }
}
