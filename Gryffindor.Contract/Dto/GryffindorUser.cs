using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class GryffindorUser
    {
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        public Guid Token { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
        [DataMember]
        public string PasswordSalt { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public bool IsLocked { get; set; }
        [DataMember]
        public bool IsSuspended { get; set; }
        [DataMember]
        public bool IsEmailConfirmed { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
    }
}
