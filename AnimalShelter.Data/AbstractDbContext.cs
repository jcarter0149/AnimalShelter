using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace AnimalShelter.Data
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext(DbContextOptions options) : base(options) { }
        public DbSet<AnimalDataEntity> Animals { get; set; }
        public DbSet<AnimalGenderDataEntity> AnimalGenders { get; set; } 
        public DbSet<AnimalTypeDataEntity> AnimalTypes { get; set; }
        public DbSet<RescueTypeDataEntity> RescueTypes { get; set;}
        public DbSet<BreedRescueTypeDataEntity> RescueBreeds { get; set; }
        public DbSet<BreedDataEntity> Breeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePrimaryKeys(modelBuilder);
            ConfigureForeignKeys(modelBuilder);
            ConfigureRequiredFields(modelBuilder);

            #region Gender Data
            modelBuilder.Entity<AnimalGenderDataEntity>().HasData(new AnimalGenderDataEntity { Id = 1, Gender = "Male" });
            modelBuilder.Entity<AnimalGenderDataEntity>().HasData(new AnimalGenderDataEntity { Id = 2, Gender = "Female" });
            #endregion
            
            #region Animal Type Data
            modelBuilder.Entity<AnimalTypeDataEntity>().HasData(new AnimalTypeDataEntity { Id = 1, Type = "Dog" });
            modelBuilder.Entity<AnimalTypeDataEntity>().HasData(new AnimalTypeDataEntity { Id = 2, Type = "Cat" });
            modelBuilder.Entity<AnimalTypeDataEntity>().HasData(new AnimalTypeDataEntity { Id = 3, Type = "Other" });
            #endregion
            
            #region Rescue Type Data
            modelBuilder.Entity<RescueTypeDataEntity>().HasData(new RescueTypeDataEntity { Id = 1, Type = "Water" });
            modelBuilder.Entity<RescueTypeDataEntity>().HasData(new RescueTypeDataEntity { Id = 2, Type = "Mountain" });
            modelBuilder.Entity<RescueTypeDataEntity>().HasData(new RescueTypeDataEntity { Id = 3, Type = "Disaster" });
            #endregion


        }

        private void ConfigurePrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalDataEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AnimalGenderDataEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<RescueTypeDataEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AnimalTypeDataEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<BreedDataEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<BreedRescueTypeDataEntity>().HasNoKey();
        }
        private void ConfigureForeignKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalDataEntity>()
                .HasOne(x => x.AnimalGender)
                .WithMany(x => x.Animals)
                .HasForeignKey(x => x.AnimalGenderId);
        }
        private void ConfigureRequiredFields(ModelBuilder modelBuilder)
        {
            void Required<TEntity>(Expression<Func<TEntity, object>> propertyToMakeRequired) where TEntity : class => modelBuilder.Entity<TEntity>()
                .Property(propertyToMakeRequired)
                .IsRequired();

            #region AnimalDataEntity
            Required<AnimalDataEntity>(x => x.Age);
            Required<AnimalDataEntity>(x => x.AnimalGenderId);
            Required<AnimalDataEntity>(x => x.Birthday);
            Required<AnimalDataEntity>(x => x.Name);
            Required<AnimalDataEntity>(x => x.AnimalNumber);
            Required<AnimalDataEntity>(x => x.InProcessDate);
            #endregion

            #region AnimalGenderDataEntity 
            Required<AnimalGenderDataEntity>(x => x.Gender);
            #endregion

            #region RescueTypeDataEntity
            Required<RescueTypeDataEntity>(x => x.Type);
            #endregion

            #region AnimalTypeDataEntity
            Required<AnimalTypeDataEntity>(x => x.Type);
            #endregion
            
            #region RescueBreedTypes
            Required<BreedDataEntity>(x => x.Name);
            #endregion
        }
    }
}
