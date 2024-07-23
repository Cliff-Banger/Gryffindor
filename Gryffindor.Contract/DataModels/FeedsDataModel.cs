using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Contract.DataModels
{
    public class FeedsDataModel
    {
        public List<Feed> Feeds { get; set; }
        public UserProfieDetails UserProfileDetails { get; set; }
    }
}
