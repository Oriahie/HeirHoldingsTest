using HeirsHolding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Infrastructure.DTO
{
    public class PersonalProgressDTO
    {
        public string PersonId { get; set; }
        public string Name { get; set; }
        public List<Courses> Courses { get; set; }
        public double AverageScore { get; set; }
    }
}
