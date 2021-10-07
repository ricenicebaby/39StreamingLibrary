using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _39StreamingLibrary.Models
{
    public class DisplayGameModel
    {
        [Required]
        [StringLength(200, ErrorMessage = "Game name is too long")]
        [MinLength(1, ErrorMessage = "Game name is too short")]
        public string GameName { get; set; } = "Game name here";

        [Required]
        [Url]
        public string GameCoverUrl { get; set; } = "Game cover link here";
    }
}
