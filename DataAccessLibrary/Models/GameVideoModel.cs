using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class GameVideoModel
    {
        [Key]
        public Guid GameVideoId { get; set; }
        public Guid GameId { get; set; }
        public string VideoUrl { get; set; }
        public DateTime VideoDate { get; set; }

    }
}
