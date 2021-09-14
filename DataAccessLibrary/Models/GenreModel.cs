using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class GenreModel
    {
        [Key]
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
