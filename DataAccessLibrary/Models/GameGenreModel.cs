using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class GameGenreModel
    {
        [Key]
        public Guid GameGenreId { get; set; }

        public Guid GameId { get; set; }

        public Guid GenreId { get; set; }

    }
}
