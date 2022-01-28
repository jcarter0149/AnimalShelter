using AnimalShelter.Data;
using AnimalShelter.Web.CustomFilters;
using AnimalShelter.Web.Models.Requests;
using AnimalShelter.Web.Models.Responses;
using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelter.Web.ApplicationServices
{
    public class AccountApplicationService
    {
        private readonly DbContext _context;
        private readonly ILogger<AccountApplicationService> _logger;
        private readonly QueriesConnectionString _queriesConnectionString;

        public AccountApplicationService(DbContext context,
            ILogger<AccountApplicationService> logger,
            QueriesConnectionString queriesConnectionString)
        {
            _context = context;
            _logger = logger;
            _queriesConnectionString = queriesConnectionString;
        }

        public Result<AccountLookupResponse> GetAccountBy(string username)
        {
            const string sql = @"SELECT a.Id, a.Username, a.RoleId, r.Name AS RoleName, r.Description AS RoleDescription
                                FROM Accounts AS a
                                JOIN Roles AS r ON a.RoleId = r.Id
                                WHERE a.Username = @Username";

            var parameters = new
            {
                Username = username
            };

            AccountLookupResponse accountResponse;

            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                accountResponse = connection.QuerySingleOrDefault<AccountLookupResponse>(sql, parameters);
            }

            if (accountResponse == null)
            {
                _logger.LogError("Attempt to retrieve the account for user {currentUser} found no result.",
                    username);

                return Result.Failure<AccountLookupResponse>($"Unable to find an Account for current user.");
            }

            _logger.LogInformation("Successfully retrieved the account for user {currentUser}.",
                username);

            return Result.Success(accountResponse);
        }

        public async Task<Result<List<AccountLookupResponse>>> GetAllAccounts()
        {
            const string sql = @"SELECT a.Id, a.Username, a.RoleId, r.Name AS RoleName, r.Description AS RoleDescription
                                FROM Accounts AS a
                                JOIN Roles AS r ON a.RoleId = r.Id";


            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                var accountResponse = await connection.QueryAsync<AccountLookupResponse>(sql);

                if (accountResponse == null)
                {
                    _logger.LogError("Attempt to retrieve the all accounts found no results");

                    return Result.Failure<List<AccountLookupResponse>>($"No accounts found");
                }

                return Result.Success(accountResponse.ToList());
            }
        }

        public Result<AccountLookupResponse> GetAccountById(Guid Id)
        {

            const string sql = @"SELECT a.Id, a.Username, a.RoleId, r.Name AS RoleName, r.Description AS RoleDescription
                                FROM Accounts AS a
                                JOIN Roles AS r ON a.RoleId = r.Id
                                WHERE a.Id = @Id";

            var parameters = new
            {
                Id = Id
            };

            AccountLookupResponse accountResponse;

            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                accountResponse = connection.QuerySingleOrDefault<AccountLookupResponse>(sql, parameters);
            }

            if (accountResponse == null)
            {
                _logger.LogError("Attempt to retrieve the account for user {currentUser} found no result.",
                    Id);

                return Result.Failure<AccountLookupResponse>($"Unable to find an Account for current user.");
            }

            _logger.LogInformation("Successfully retrieved the account for user {currentUser}.",
                Id);

            return Result.Success(accountResponse);
        }
        public async Task<Result> UpdateRole(UpdateAccountRequest request)
        {
            var account = await _context.Set<AccountDataEntity>()
                .Where(x => x.Id == request.Id)
                   .FirstOrDefaultAsync();
            if (account == null)
            {
                return Result.Failure($"No account found with Id {request.Id}");
            }
            account.RoleId = request.RoleId;
            await _context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> CreateRole(CreateAccountRequest request)
        {
            var account = await _context.Set<AccountDataEntity>()
                .Where(x => x.Username == request.Username)
                .FirstOrDefaultAsync();

            if (account != null)
            {
                return Result.Failure($"User already has an account with username {request.Username}");
            }

            var newAccount = new AccountDataEntity()
            {
                Username = request.Username,
                RoleId = request.RoleId
            };
            _context.Add(newAccount);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
