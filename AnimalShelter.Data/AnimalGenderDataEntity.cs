using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Data
{
    public class AnimalGenderDataEntity
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public ICollection<AnimalDataEntity> Animals { get; set; }
    }
}
