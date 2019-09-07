using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserApp.Models
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}