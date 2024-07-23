using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.Dto
{
    [DataContract]
    public class Notification
    {
        [DataMember]
        public Guid NotificationId { get; set; }
        [DataMember]
        public Guid Recipient { get; set; }
        [DataMember]
        public Guid Sender { get; set; }
        [DataMember]
        public string SenderName { get; set; }
        [DataMember]
        public string SenderUsername { get; set; }
        [DataMember]
        public DateTime SentOn { get; set; }
        [DataMember]
        public int NotificationType { get; set; }
        [DataMember]
        public string NotificationLink { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public bool IsRead { get; set; }
    }
}
