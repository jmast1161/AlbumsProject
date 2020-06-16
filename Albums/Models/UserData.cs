using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albums.Models
{
    public class UserData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public AddressData Address { get; set; }
        
        public string Phone { get; set; }
        
        public string Website { get; set; }

        public CompanyData Company { get; set; }
    }
}
