using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class UserFollowing
    {
        [DataMember]
        public Guid UserFollowingId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        public Guid SelectedUserId { get; set; }
    }
}
