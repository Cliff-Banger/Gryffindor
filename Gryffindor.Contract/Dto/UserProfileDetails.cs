using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class UserProfieDetails
    {
        [DataMember]
        public Guid UserAccessId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        public bool CanViewPrivateProfiles { get; set; }
        [DataMember]
        public bool CanPostJobs { get; set; }
        [DataMember]
        public int HasEducation { get; set; }
        [DataMember]
        public int HasSkills { get; set; }
        [DataMember]
        public int HasWorkExperience { get; set; }
        [DataMember]
        public int HasAchievements { get; set; }
        [DataMember]
        public bool HasPersonalDetails { get; set; }
        [DataMember]
        public bool IsProfileComplete { get; set; }
        [DataMember]
        public bool CanApplyForSchool { get; set; }
        [DataMember]
        public bool CanApplyForJobs { get; set; }
    }
}
