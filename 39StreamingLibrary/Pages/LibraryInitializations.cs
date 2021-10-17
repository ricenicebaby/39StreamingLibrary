using _39StreamingLibrary.Models;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _39StreamingLibrary.Pages
{
    public partial class Library
    {
        // Booleans for opening or closing modals
        public bool ShowPopup { get; set; } = false;
        public bool ShowEditPopup { get; set; } = false;
        public bool ShowDeletePopup { get; set; } = false;

        // Initializations
        private List<GameModel> games; // Holds currently populated list of games
        private DisplayGameModel newGame = new DisplayGameModel(); // For new games
        GameModel selectedGame = new GameModel();
        List<GameModel> specificGames = new List<GameModel>();

        private List<GenreModel> genres; // Holds currently populated list of genres
        private DisplayGenreModel newGenre = new DisplayGenreModel(); // For new genres
        GenreModel selectedGenre = new GenreModel();
        List<GenreModel> specificGenres = new List<GenreModel>();

        private List<GameGenreModel> gamegenres;
        private DisplayGameGenreModel newGameGenre = new DisplayGameGenreModel(); //
        public List<GameGenreModel> testggmodel = new List<GameGenreModel>(); // For testing purposes

        private List<GameVideoModel> gamevideos;
        GameVideoModel selectedVideo = new GameVideoModel();
        List<GameVideoModel> specificVideos = new List<GameVideoModel>();

                Regex rxInsensitive = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}
