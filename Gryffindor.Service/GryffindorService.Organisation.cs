using Gryffindor.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Gryffindor.Contract.Dto;
using System.ServiceModel;
using log4net;
using System.Data.SqlClient;
using Gryffindor.Service.Utilities;
using Dapper;
using Gryffindor.Contract.Utilities;

namespace Gryffindor.Service
{
    public partial class GryffindorService : IGryffindorService
    {
        [OperationBehavior]
        public GryffindorResponse<Organisation> GetOrganisation(Guid organisationId)
        {
            var result = new GryffindorResponse<Organisation>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@OrganisationId", organisationId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<Organisation>("up_Organisation_GetOrganisation", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with GetOrganisation", e);
                result.Message = "Error with GetOrganisation: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<IList<Organisation>> GetOrganisations()
        {
            var result = new GryffindorResponse<IList<Organisation>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<Organisation>("up_Organisation_GetOrganisations", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with GetOrganisations", e);
                result.Message = "Error with GetOrganisations: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> AddOrUpdateOrganisation(Organisation organisation)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@OrganisationId", organisation.OrganisationId);
                parameters.Add("@Name", organisation.Name);
                parameters.Add("@Description", organisation.Description);
                parameters.Add("@OrganisationType", organisation.OrganisationType);
                parameters.Add("@ApplicationUrl", organisation.ApplicationUrl);
                parameters.Add("@Telephone", organisation.Telephone);
                parameters.Add("@Email", organisation.Email);
                parameters.Add("@Website", organisation.Website);
                parameters.Add("@Address", organisation.Address);
                parameters.Add("@LogoPath", organisation.LogoPath);
                parameters.Add("@MainPicturePath", organisation.MainPicturePath);
                parameters.Add("@Tags", organisation.Tags);
                parameters.Add("@GryffindorUserId", organisation.CreatedBy);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_Organisation_AddOrUpdateOrganisation", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;           
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error with AddOrUpdateOrganisation", e);
                result.Message = "Error with AddOrUpdateOrganisation: " + e.Message;
                return result;
            }
        }
    }
}
