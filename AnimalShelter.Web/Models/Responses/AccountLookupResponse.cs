using System;

namespace AnimalShelter.Web.Models.Responses
{
    public class AccountLookupResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
