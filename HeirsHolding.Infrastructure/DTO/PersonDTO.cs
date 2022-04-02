using HeirsHolding.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Infrastructure.DTO
{
    public class PersonDTO
    {
        [Required]
        public List<Persons> Persons { get; set; }
    }
}
