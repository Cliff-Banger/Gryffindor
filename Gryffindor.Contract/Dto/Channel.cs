using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class Channel
    {
        [DataMember]
        public Guid ChannelId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }
    }
}
