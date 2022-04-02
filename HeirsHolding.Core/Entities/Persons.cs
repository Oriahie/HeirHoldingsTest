using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Core.Entities
{
    public class Persons
    {
        [Required(ErrorMessage = "Person Id is Required")]
        public string Person_Id { get; set; }
        [Required(ErrorMessage = "Course Id is required")]
        public string Course_Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Range(0, 5, ErrorMessage = "Provided Score is Invalid")]
        public int Score { get; set; }
    }
}
