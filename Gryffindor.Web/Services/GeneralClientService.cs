using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gryffindor.Contract.Dto;
using log4net;
using Gryffindor.Contract;
using Gryffindor.Contract.Utilities;

namespace Gryffindor.Web.Services
{
    public class GeneralClientService : IGeneralClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));

        public IList<Profession> GetProfessions()
        {
            IList<Profession> result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetProfessions().Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get professions", e);
                return result;
            }
        }
    }
}