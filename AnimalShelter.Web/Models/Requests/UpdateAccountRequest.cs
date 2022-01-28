using System;

namespace AnimalShelter.Web.Models.Requests
{
    public class UpdateAccountRequest
    {
        internal int roleId;

        public Guid Id { get; set; }
        public int RoleId { get; set; }
    }
}
