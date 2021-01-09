using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso_AspMVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string Description { get; set; }
      
    }
}
