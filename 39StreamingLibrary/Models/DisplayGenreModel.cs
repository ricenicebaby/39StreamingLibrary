using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _39StreamingLibrary.Models
{
    public class DisplayGenreModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Genre tag is too long!")]
        [MinLength(1, ErrorMessage = "Genre tag is too short!")]
        public string GenreName { get; set; }

    }
}
