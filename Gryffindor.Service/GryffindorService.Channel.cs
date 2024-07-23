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
        public GryffindorResponse<IList<Channel>> GetAllChannels()
        {
            var result = new GryffindorResponse<IList<Channel>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<Channel>("up_Channel_GetAllChannels", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling GetAllChannels", e);
                result.Message = "Error calling GetAllChannels: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<IList<Channel>> GetUserChannels(Guid userId)
        {
            var result = new GryffindorResponse<IList<Channel>>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    result.Data = connection.Query<Channel>("up_Channel_GetUserChannels", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure).ToList();
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling GetUserChannels", e);
                result.Message = "Error calling GetUserChannels: " + e.Message;
                return result;
            }
        }

        [OperationBehavior]
        public GryffindorResponse<bool> AddOrRemoveUserChannels(Guid userId, List<Guid> channelsToRemove, List<Guid> channelsToAdd)
        {
            var result = new GryffindorResponse<bool>();
            SqlConnection connection;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GryffindorUserId", userId);

                var channelsToAddDataTable = new DataTable();
                channelsToAddDataTable.Columns.Add("ID", typeof(Guid));
                foreach (var item in channelsToAdd)
                    channelsToAddDataTable.Rows.Add(item);

                var channelsToRemoveDataTable = new DataTable();
                channelsToRemoveDataTable.Columns.Add("ID", typeof(Guid));
                foreach (var item in channelsToRemove)
                    channelsToRemoveDataTable.Rows.Add(item);

                parameters.Add("@ChannelsToAddTVP", channelsToAddDataTable.AsTableValuedParameter("TVP_GUID"));
                parameters.Add("@ChannelsToRemoveTVP", channelsToRemoveDataTable.AsTableValuedParameter("TVP_GUID"));

                using (connection = new SqlConnection(DatabaseUtilities.ConnectionString))
                {
                    connection.Execute("up_Channel_AddorRemoveUserChannels", parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
                    result.Data = true;
                    result.Result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                _log.Error("Error calling AddOrRemoveUserChannels", e);
                result.Message = "Error calling AddOrRemoveUserChannels: " + e.Message;
                return result;
            }
        }
    }
}
