using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 35)]
        public string Title1 { get; set; }
        public int Order { get; set; }
       
        [StringLength(maximumLength: 35)]
        public string Title2 { get; set; }
        [StringLength(maximumLength: 250)]
        public string Text { get; set; }
        [StringLength(maximumLength: 35)]
        public string BtnText { get; set; }
        [StringLength(maximumLength: 250)]
        public string BtnUrl { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }

        [NotMapped] 
        public IFormFile ImageFile { get; set; }
    }
}
