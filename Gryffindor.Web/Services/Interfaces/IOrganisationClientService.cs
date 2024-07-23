using Gryffindor.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Services
{
    public interface IOrganisationClientService
    {
        Organisation GetOrganisation(Guid organisationId);

        IList<Organisation> GetOrganisations();

        bool AddOrUpdateOrganisation(Organisation organisation);
    }
}