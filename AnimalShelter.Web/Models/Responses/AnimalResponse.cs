using System;

namespace AnimalShelter.Web.Models.Responses
{
    public class AnimalResponse
    {
        public Guid Id { get; set; }
        public string AnimalNumber { get; set; }
        public decimal Age { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime InProcessDate { get; set; }
        public decimal LocationLatitude { get; set; }
        public decimal LocationLongitude { get; set; }
        public decimal AgeInWeeks => Age * 52;
        public int AnimalGenderId { get; set; }
        public string AnimalGender { get; set; }
    }
}
