using _39StreamingLibrary.Models;
using DataAccessLibrary.Models;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _39StreamingLibrary.Pages
{
    public partial class Library
    {
        // Populate models. Read
        protected override async Task OnInitializedAsync()
        {
            string sqlgame = "select GameId, GameName, GameCoverUrl, GameDescription, GameDate from dbo.Game";
            games = await _db.LoadData<GameModel, dynamic>(sqlgame, new { });

            string sqlgenre = "select GenreId, GenreName from dbo.Genre";
            genres = await _db2.LoadData<GenreModel, dynamic>(sqlgenre, new { });
            genres.Sort((x, y) => x.GenreName.CompareTo(y.GenreName));

            string sqlgamegenre = "select GameId, GenreId from dbo.GameGenre";
            gamegenres = await _db3.LoadData<GameGenreModel, dynamic>(sqlgamegenre, new { });

            string sqlgamevideo = "select GameVideoId, GameId, VideoUrl, VideoDate from dbo.GameVideo";
            gamevideos = await _db4.LoadData<GameVideoModel, dynamic>(sqlgamevideo, new { });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await jsRuntime.InvokeVoidAsync("renderjQueryComp");
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        List<GameModel> FilteredGames => games.Where(img => img.GameName.ToLower().Contains(SearchText.ToLower())).ToList();

        void OpenPopup(GameModel currGame)
        {
            // Set the selected game as the current game
            selectedGame = currGame;
            grabSpecificGenres(currGame);
            grabSpecificVideos(currGame);
        }
        void ClosePopup()
        {
            // Close the modal
            ShowPopup = false;

            // Have to clear the list, otherwise genres and videos just keep adding up with each opened modal
            specificGenres.Clear();
            specificVideos.Clear();
        }

        // Grab and populate the associated GENRES with the selected GAME
        void grabSpecificGenres(GameModel currGame)
        {
            List<GenreModel> set1 = new List<GenreModel>();
            int statusArr = 0;
            foreach (var gamegenre in gamegenres)
            {
                if (currGame.GameId == gamegenre.GameId)
                {
                    foreach (var genre in genres)
                    {
                        if (gamegenre.GenreId == genre.GenreId)
                        {
                            set1.Add(genre);
                            if (genre.GenreName == "Completed")
                                statusArr = 1;
                            else if (genre.GenreName == "Ongoing")
                                statusArr = 2;
                            else if (genre.GenreName == "Flushed")
                                statusArr = 3;
                        }
                    }
                }
            }

            string gameStatus = "";
            if (statusArr != 0)
            {
                switch (statusArr)
                {
                    case 1:
                        gameStatus = "Completed";
                        break;
                    case 2:
                        gameStatus = "Ongoing";
                        break;
                    case 3:
                        gameStatus = "Flushed";
                        break;
                }

                int statusIndex = set1.FindIndex(x => x.GenreName == gameStatus);
                GenreModel item = set1[statusIndex];
                for (int i = statusIndex; i > 0; i--)
                    set1[i] = set1[i - 1];
                set1[0] = item;
            }

            specificGenres = set1;

        }

        // Grab and populate the associated GAMES with the selected GENRES
        public async Task grabSpecificGames(GenreModel currGenre, object checkedValue)
        {
            // Set 1 contains ALL GameGenre ENTRIES with the current genre's ID
            var set1 = gamegenres.Where(gamegenre => gamegenre.GenreId == currGenre.GenreId);

            // Intersect the freshly made gamegenre set (first) with ALL game entries.
            // Set 2 contains all GAME ID'S that are associated with the current genre
            var set2 = games.Select(x => x.GameId).Intersect(set1.Select(x => x.GameId));  

            // Finally, set 3 contains all Game ENTRIES from set 2. i.e. Set 3 has actual games and not just their ID's like set 2
            var set3 = games.Where(x => set2.Contains(x.GameId));

            // If the checkbox is checked
            if ((bool)checkedValue)
            {
                specificGames = set3.ToList(); 
                games = specificGames;
            }

            // If the checkbox is unchecked
            else
            {
                foreach (GameModel set3game in set3.ToList())
                {
                    specificGames.Remove(set3game);
                }

                await OnInitializedAsync();
                await InvokeAsync(StateHasChanged);
            }
        }

        // Grabs specific videos associated with the current game
        void grabSpecificVideos(GameModel currGame)
        {
            var set1 = gamevideos.Where(gamevideo => gamevideo.GameId == currGame.GameId); 
            specificVideos = set1.ToList();
            specificVideos.Sort((x, y) => x.VideoDate.CompareTo(y.VideoDate));
        }

        // Open edit popup
        void OpenEditGame(GameModel currGame)
        {
            selectedGame = currGame;
            grabSpecificGenres(currGame);
            grabSpecificVideos(currGame);
            ShowEditPopup = true;
        }

        // Open the delete popup
        void OpenDeletePopup(GameModel currGame)
        {
            // Open the popup prompting a delete
            ShowDeletePopup = true;
            selectedGame = currGame;
        }

        void Sort(int specifiedSort)
        {
            switch (specifiedSort)
            {
                case 1:
                    games.Sort((x, y) => x.GameName.CompareTo(y.GameName));
                    break;
                case 2:
                    games.Sort((x, y) => y.GameName.CompareTo(x.GameName));
                    break;
                case 3:
                    games.Sort((x, y) => x.GameDate.CompareTo(y.GameDate));
                    break;
                case 4:
                    games.Sort((x, y) => y.GameDate.CompareTo(x.GameDate));
                    break;
            }
        }

        private async Task SortStatus(int status)
        {
            string sqlgame = "select GameId, GameName, GameCoverUrl, GameDescription, GameDate from dbo.Game";

            string statusName = "";
            switch (status)
            {
                case 1:
                    statusName = "Completed";

                    break;
                case 2:
                    statusName = "Ongoing";

                    break;
                case 3:
                    statusName = "Flushed";

                    break;
                case 4:
                    games = await _db.LoadData<GameModel, dynamic>(sqlgame, new { });
                    return;
            }

            // This set contains all genre entries with the current genre's name
            var genreset = genres.FirstOrDefault(genre => genre.GenreName == statusName);
            // Set 1 contains ALL GameGenre ENTRIES with the current genre's ID
            var set1 = gamegenres.Where(gamegenre => gamegenre.GenreId == genreset.GenreId);

            // Intersect the freshly made gamegenre set (first) with ALL game entries.
            // Set 2 contains all GAME ID'S that are associated with the current genre
            var set2 = games.Select(x => x.GameId).Intersect(set1.Select(x => x.GameId));

            // Finally, set 3 contains all Game ENTRIES from set 2. i.e. Set 3 has actual games and not just their ID's like set 2
            var set3 = games.Where(x => set2.Contains(x.GameId));

            games = set3.ToList();
        }

    }
}
