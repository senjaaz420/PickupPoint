using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDiplom.Models
{
    public class Order
    {
     public int Id { get; set; }
     public string IsPaid { get; set; }
     public double Summary { get; set; }
     public DateTime OrderTime { get; set; }
    }
}
