using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class Profession
    {
        [DataMember]
        public Guid ProfessionId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Field { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }
}
