using AnimalShelter.Data;
using AnimalShelter.Web.Models.Requests;
using AnimalShelter.Web.Models.Responses;
using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelter.Web.ApplicationServices
{
    public class AnimalApplicationService
    {
        private readonly DbContext _context;
        private readonly ILogger<AnimalApplicationService> _logger;
        private readonly QueriesConnectionString _queriesConnectionString;

        public AnimalApplicationService(DbContext context,
                ILogger<AnimalApplicationService> logger,
                QueriesConnectionString queriesConnectionString)
        {
            _context = context;
            _logger = logger;
            _queriesConnectionString = queriesConnectionString;
        }

        public async Task<Result<List<AnimalResponse>>> GetAll()
        {
            const string query = @"SELECT * FROM Animals";


            using(var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                var result = await connection.QueryAsync<AnimalResponse>(query);

                _logger.LogInformation($"Returned {result.Count()} animals from database");

                return Result.Success(result.ToList());
            }
        }

        public async Task<Result<AnimalResponse>> GetById(Guid id)
        {
            const string query = @"SELECT * FROM Animals WHERE Id = @id";

            var param = new { id };


            using (var connection = new SqlConnection(_queriesConnectionString.Value))
            {
                var result = await connection.QueryFirstOrDefaultAsync<AnimalResponse>(query, param);

                _logger.LogInformation($"Returned {result.Id}, {result.Name} from database");

                return Result.Success(result);
            }
        }

        public async Task<Result> Save(AnimalSaveRequest animal)
        {
            var animalEntity = _context.Animals.FirstOrDefault(x => x.AnimalNumber == animal.AnimalNumber);
            if (animalEntity != null)
            {
                return Result.Failure($"Animal with Animal Number ${animal.AnimalNumber} already exists");
            }

            var newAnimal = new AnimalDataEntity()
            {
                AnimalNumber = animal.AnimalNumber,
                Age = animal.Age,
                Name = animal.Name,
                Birthday = animal.Birthday,
                LocationLatitude = animal.LocationLatitude,
                LocationLongitude = animal.LocationLongitude,
                AnimalGenderId = animal.AnimalGenderId
            };

            _context.Add(newAnimal);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> Update(AnimalUpdateRequest animal)
        {
            var animalEntity = _context.Animals.FirstOrDefault(x => x.Id == animal.Id);
            
            animalEntity.Age = animal.Age;
            animalEntity.Name = animal.Name;
            animalEntity.Birthday = animal.Birthday;
            animalEntity.LocationLatitude = animal.LocationLatitude;
            animalEntity.LocationLongitude = animal.LocationLongitude;
            animalEntity.AnimalGenderId = animal.AnimalGenderId;

            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> Delete(Guid id)
        {
            var animalEntity = _context.Animals.FirstOrDefault(x => x.Id == id);

            _context.Animals.Remove(animalEntity);

            await _context.SaveChangesAsync();  

            return Result.Success();
        }
    }
}
