using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Data
{
    public class BreedRescueTypeDataEntity
    {
        public ICollection<BreedDataEntity> Breed { get; set; }
        public int BreedTypeId { get; set; }
        public int RescueTypeId { get; set; }
        public ICollection<RescueTypeDataEntity> RescueType { get; set; }
    }
}
