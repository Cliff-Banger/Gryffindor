using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class ResumePermission
    {
        [DataMember]
        public Guid ResumePermissionId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        public Guid? SharedToUserId { get; set; }
        [DataMember]
        public string SharedToEmail { get; set; }
        [DataMember]
        public bool ShareFullResume { get; set; }
        [DataMember]
        public int Downloads { get; set; }
        [DataMember]
        public DateTime? LastDownloaded { get; set; }
        [DataMember]
        public bool IsBlocked { get; set; }
    }
}
