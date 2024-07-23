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
    public class OrganisationClientService : IOrganisationClientService
    {
        private ILog _log = LogManager.GetLogger(typeof(SecurityClientService));

        public bool AddOrUpdateOrganisation(Organisation organisation)
        {
            bool result = false;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.AddOrUpdateOrganisation(organisation).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling add or update organisation", e);
                return result;
            }
        }

        public Organisation GetOrganisation(Guid organisationId)
        {
            Organisation result = null;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetOrganisation(organisationId).Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get get organisation", e);
                return result;
            }
        }

        public IList<Organisation> GetOrganisations()
        {
            IList<Organisation> result;
            try
            {
                using (var proxy = new ServiceProxy<IGryffindorService>())
                {
                    result = proxy.Channel.GetOrganisations().Data;
                    return result;
                }
            }
            catch (Exception e)
            {
                _log.Error("Error calling get get organisations", e);
                return new List<Organisation>();
            }
        }
    }
}