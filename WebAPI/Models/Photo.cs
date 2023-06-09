using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("Photos")]
    public class Photo : BaseEntity
    {
        [Required]
        public string ImageURL { get; set; }
        public bool IsPrimary {get; set; }
        public int PropertyID { get; set; }
        public Property Property { get; set; }
    }
}