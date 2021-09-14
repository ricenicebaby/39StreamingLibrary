using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class GameModel
    {
        [Key]
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public string GameCoverUrl { get; set; }
    }
}
