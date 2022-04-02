using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Core.Entities
{
    public class Courses
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

}
