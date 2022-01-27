using System;

namespace AnimalShelter.Web.Models.Requests
{
    public class AnimalUpdateRequest
    {
        public Guid Id { get; set; }
        public string AnimalNumber { get; set; }
        public decimal Age { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public decimal LocationLatitude { get; set; }
        public decimal LocationLongitude { get; set; }
        public int AnimalGenderId { get; set; }
    }
}
