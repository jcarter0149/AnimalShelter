using AnimalShelter.Domain;
using AnimalShelter.Web.Models.Responses;
using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AnimalShelter.Web.ApplicationServices
{
    public sealed class RoleApplicationService
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<RoleApplicationService> _logger;

        public RoleApplicationService(QueriesConnectionString queriesConnectionString,
          ILogger<RoleApplicationService> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public Result<bool> IsUserAnAssistant(string username)
        {
            const string sql = @"SELECT Username
                                      , RoleId
                                 FROM Accounts
                                 WHERE Username = @Username AND RoleId = @RoleId";

            var parameters = new
            {
                Username = username,
                RoleId = (int)Enums.Role.Assistant
            };

            UserRoleLookupResponse userRoleLookupResponse;

            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                userRoleLookupResponse = connection.QuerySingleOrDefault<UserRoleLookupResponse>(sql, parameters);
            }


            return Result.Success(userRoleLookupResponse != null);
        }

        public Result<bool> IsUserAnAdminOrAssistant(NotNullOrEmptyString username)
        {

            const string sql = @"SELECT Username
                                      , RoleId
                                 FROM Accounts
                                 WHERE Username = @Username AND (RoleId = @AdminRoleId OR RoleId = @AssistantRoleId)";

            var parameters = new
            {
                Username = username.Value,
                AdminRoleId = (int)Enums.Role.Admin,
                AssistantRoleId = (int)Enums.Role.Assistant
            };

            UserRoleLookupResponse userRoleLookupResponse;

            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                userRoleLookupResponse = connection.QuerySingleOrDefault<UserRoleLookupResponse>(sql, parameters);
            }


            return Result.Success(userRoleLookupResponse != null);
        }

        public Result<bool> IsUserAnAdmin(string username)
        {
            const string sql = @"SELECT Username
                                      , RoleId
                                 FROM Accounts
                                 WHERE Username = @Username AND RoleId = @RoleId";

            var parameters = new
            {
                Username = username,
                RoleId = (int)Enums.Role.Admin
            };

            UserRoleLookupResponse userRoleLookupResponse;

            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                userRoleLookupResponse = connection.QuerySingleOrDefault<UserRoleLookupResponse>(sql, parameters);
            }


            return Result.Success(userRoleLookupResponse != null);
        }

        public Result<List<RoleResponse>> GetAllRoles()
        {
            const string sql = @"SELECT Id, Name, Description FROM Roles";

            List<RoleResponse> rolesLookupResponse;

            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                rolesLookupResponse = connection.Query<RoleResponse>(sql).ToList();
            }

            if (rolesLookupResponse.Count == 0)
            {
                return Result.Failure<List<RoleResponse>>("This should never be zero, you are are missing roles in DB");
            }

            return Result.Success(rolesLookupResponse);
        }

    }
}
