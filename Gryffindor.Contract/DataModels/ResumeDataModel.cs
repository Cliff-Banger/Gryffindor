using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.DataModels
{
    public class ResumeDataModel
    {
        public UserProfile UserProfile { get; set; }
        public List<UserProfileAchievement> Achievements { get; set; }
        public List<UserProfileEducation> Education { get; set; }
        public List<UserProfileSkill> Skills { get; set; }
        public List<UserProfileWork> Work { get; set; }
    }
}
