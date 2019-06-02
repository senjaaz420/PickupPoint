using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDiplom.Models;

namespace DDDiplom.Models
{
    public class WorkPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UsersId { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public List<WorkPlace> WorkPlaces { get; set; }
        public WorkPlace()
        {
            WorkPlaces = new List<WorkPlace>();
        }
    }

    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public List<WorkPlace> WorkPlaces { get; set; }
        public Address()
        {
            WorkPlaces = new List<WorkPlace>();
        }

    }
}
