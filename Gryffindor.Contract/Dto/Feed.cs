using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class Feed
    {
        [DataMember]
        public Guid FeedId { get; set; }
        [DataMember]
        public Guid GryffindorUserId { get; set; }
        [DataMember]
        public int FeedTypeId { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Avatar { get; set; }
        [DataMember]
        public string FeedTypeDescription { get; set; }
        [DataType(DataType.MultilineText)]
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public string RedirectUrl { get; set; }
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public int Likes { get; set; }
        [DataMember]
        public int Liked { get; set; }
        [DataMember]
        public int Interests { get; set; }
        [DataMember]
        public int Interested { get; set; }
        [DataMember]
        public DateTime CreatedOn { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid ModifiedBy { get; set; }

        [DataMember]
        public Guid JobFeedId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string Salary { get; set; }
        [DataMember]
        public string ContactEmail { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }
    }
}
