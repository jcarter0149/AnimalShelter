using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Data
{
    public class AnimalDataEntity
    {
        public Guid Id { get; set; }
        public string AnimalNumber { get; set; }
        public decimal Age { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime InProcessDate { get; set; } 
        public decimal LocationLatitude { get; set; }
        public decimal LocationLongitude { get; set; }
        public int AnimalGenderId { get; set; }
        public int Type { get; set; }
        public AnimalTypeDataEntity AnimalType { get; set; }
        public AnimalGenderDataEntity AnimalGender { get; set; }
        public BreedDataEntity Breed { get; set; }
        public int BreedId { get; set; }
    }
}
