using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Curso_AspMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string Description { get; set; }
        [Range(1, 100, ErrorMessage = "Value out of range")]
        public int Amount { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
