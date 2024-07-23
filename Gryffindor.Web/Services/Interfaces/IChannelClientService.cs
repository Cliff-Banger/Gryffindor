using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gryffindor.Web.Services
{
    public interface IChannelClientService
    {
        IList<Channel> GetAllChannels();

        IList<Channel> GetUserChannels(Guid userId);

        bool AddOrRemoveUserChannels(Guid userId, List<Guid> channelsToRemove, List<Guid> channelsToAdd);
    }
}
