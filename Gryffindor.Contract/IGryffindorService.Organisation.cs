using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;

namespace Gryffindor.Contract
{
    public partial interface IGryffindorService
    {
        [OperationContract]
        GryffindorResponse<Organisation> GetOrganisation(Guid organisationId);

        [OperationContract]
        GryffindorResponse<IList<Organisation>> GetOrganisations();

        [OperationContract]
        GryffindorResponse<bool> AddOrUpdateOrganisation(Organisation organisation);
    }
}
