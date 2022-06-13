using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 250)]
        public string Icon { get; set; }
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }
        [StringLength(maximumLength: 200)]
        public string Text { get; set; }
    }
}
