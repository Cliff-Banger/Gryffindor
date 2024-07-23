using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Utilities;
using Gryffindor.Contract;
using log4net;
using System.Threading.Tasks;

namespace Gryffindor.Web.Services
{
    public class ChannelClientService : IChannelClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));       

        public IList<Channel> GetAllChannels()
        {
            IList<Channel> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetAllChannels().Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error getting channels", e);
                return result;
            }
        }

        public IList<Channel> GetUserChannels(Guid userId)
        {
            IList<Channel> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetAllChannels().Data;
                    var userChannels = proxy.Channel.GetUserChannels(userId).Data;

                    if (userChannels.Count > 0)
                        for (var i = 0; i < result.Count; i++)
                        {
                            if (userChannels.FirstOrDefault(c => c.ChannelId == result[i].ChannelId) != null)
                                result[i].IsSelected = true;
                        }
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error getting user channels", e);
                return result;
            }
        }

        public bool AddOrRemoveUserChannels(Guid userId, List<Guid> channelsToRemove, List<Guid> channelsToAdd)
        {
            var result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrRemoveUserChannels(userId, channelsToRemove, channelsToAdd).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error updating channels", e);
                return result;
            }
        }

        public Task<bool> AddOrRemoveUserChannelsAsync(Guid userId, List<Guid> channelsToRemove, List<Guid> channelsToAdd)
        {
            return Task.Run(() =>
            {
                var result = false;
                try
                {
                    using (var proxy = new ServiceProxy<IGryffindorService>())
                    {
                        result = proxy.Channel.AddOrRemoveUserChannels(userId, channelsToRemove, channelsToAdd).Data;
                        return result;
                    }
                }
                catch (Exception e)
                {
                    _log.Error("Error updating channels", e);
                    return result;
                }
            });
        }
    }
}