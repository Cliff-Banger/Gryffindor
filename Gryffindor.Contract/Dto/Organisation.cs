using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class Organisation
    {
        [DataMember]
        public Guid OrganisationId { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        [Required]
        public string Description { get; set; }
        [DataMember]
        public int OrganisationType { get; set; }
        [DataMember]
        public string ApplicationUrl { get; set; }
        [DataMember]
        [Required]
        public string Telephone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [Required]
        [DataMember]
        public string Website { get; set; }
        [DataMember]
        [Required]
        public string Address { get; set; }
        [DataMember]
        public string LogoPath { get; set; }
        [DataMember]
        public string MainPicturePath { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public Guid CreatedBy { get; set; }
        [DataMember]
        public Guid? ModifiedBy { get; set; }
    }
}
