using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Data
{
    public class AccountDataEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public RoleDataEntity Role { get; set; }
    }
}
