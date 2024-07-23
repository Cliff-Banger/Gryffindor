using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Enums
{
    [DataContract]
    [Flags]
    public enum FeedType
    {
        [EnumMember]
        General = 1,
        [EnumMember]
        Jobs = 2,
        [EnumMember]
        Sponsored = 3,
        [EnumMember]
        Blogs = 4
    }

    [DataContract]
    [Flags]
    public enum Gender
    {
        [EnumMember]
        Male = 1,
        [EnumMember]
        Female = 2
    }

    [DataContract]
    [Flags]
    public enum NotificationType
    {
        [EnumMember]
        Connection = 0,
        [EnumMember]
        Like = 1,
        [EnumMember]
        Interest = 2,
        [EnumMember]
        Share = 3,
        [EnumMember]
        Message = 4,
        [EnumMember]
        Inception = 5
    }

    [DataContract]
    [Flags]
    public enum MainInterest
    {
        [EnumMember]
        Education = 0,
        [EnumMember]
        Jobs = 1,
        [EnumMember]
        Candidates = 2
    }

    [DataContract]
    [Flags]
    public enum OrganisationType
    {
        [EnumMember]
        School = 0,
        [EnumMember]
        Tertiary = 1,
        [EnumMember]
        Company = 2
    }

    [DataContract]
    [Flags]
    public enum QualificationType
    {
        [EnumMember]
        Education = 0,
        [EnumMember]
        Skills = 1,
        [EnumMember]
        Achievements = 2,
        [EnumMember]
        Work = 3
    }
}
