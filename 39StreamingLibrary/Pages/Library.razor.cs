using _39StreamingLibrary.Models;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _39StreamingLibrary.Pages
{
    public partial class Library
    {
        // Populate models
        protected override async Task OnInitializedAsync()
        {
            string sqlgame = "select GameId, GameName, GameCoverUrl from dbo.Game";
            games = await _db.LoadData<GameModel, dynamic>(sqlgame, new { });

            string sqlgenre = "select GenreId, GenreName from dbo.Genre";
            genres = await _db2.LoadData<GenreModel, dynamic>(sqlgenre, new { });

            string sqlgamegenre = "select GameId, GenreId from dbo.GameGenre";
            gamegenres = await _db3.LoadData<GameGenreModel, dynamic>(sqlgamegenre, new { });
        }

        void OpenGame(GameModel currGame)
        {
            // Set the selected game as the current game
            selectedGame = currGame;
            grabSpecificGenres(currGame);

            // Open the Popup
            ShowPopup = true;
        }
        void ClosePopup()
        {
            // Close the modal
            ShowPopup = false;

            // Have to clear the list, otherwise genres just keep adding up with each opened modal
            specificGenres.Clear();

            // Have to reset the offsets, otherwise position of the next modal begins when the last one was closed
            offsetY = 0;
            offsetX = 0;
        }

        // Grab and populate the associated GENRES with the selected GAME
        void grabSpecificGenres(GameModel currGame)
        {
            foreach (var gamegenre in gamegenres)
            {
                if (currGame.GameId == gamegenre.GameId)
                {
                    foreach (var genre in genres)
                    {
                        if (gamegenre.GenreId == genre.GenreId)
                        {
                            specificGenres.Add(genre);
                        }
                    }
                }
            }
        }

        // Grab and populate the associated GAMES with the selected GENRES
        public async Task grabSpecificGames(GenreModel currGenre, object checkedValue)
        {
            var set1 = gamegenres.Where(gamegenre => gamegenre.GenreId == currGenre.GenreId);
            var set2 = games.Select(x => x.GameId).Intersect(set1.Select(x => x.GameId));

            var set3 = games.Where(x => set2.Contains(x.GameId));

            // If checked
            if ((bool)checkedValue)
            {
                specificGames = set3.ToList();
                games = specificGames;
            }

            // If unchecked
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


        /// /////////////////////////////
        /// /////////////////////////////
        // CRUD ACTIONS
        //

        private async Task InsertGame()
        {
            GameModel g = new GameModel
            {
                GameName = newGame.GameName,
                GameCoverUrl = newGame.GameCoverUrl
            };

            string sql = @"insert into dbo.Game (GameName, GameCoverUrl)" +
                    "values (@GameName, @GameCoverUrl);";
            await _db.SaveData<GameModel>(sql, g);

            games.Add(g);

            await OnInitializedAsync();

            newGame = new DisplayGameModel(); // Clear up the new game
            selectedGenre = new GenreModel(); // Clear up the inserted genre, otherwise it'll automatically display when the next game is edited

        }

        void OpenEditGame(GameModel currGame)
        {
            selectedGame = currGame;
            grabSpecificGenres(currGame);
            ShowEditPopup = true;
        }

        private async Task UpdateGame()
        {
            ShowEditPopup = false;

            GameModel g = new GameModel()
            {
                GameName = selectedGame.GameName,
                GameCoverUrl = selectedGame.GameCoverUrl,
                GameId = selectedGame.GameId
            };

            specificGenres.Clear();
            string sql = "update dbo.Game set GameName = @GameName where GameId = @GameId" +
                    " update dbo.Game set GameCoverUrl = @GameCoverUrl where GameId = @GameId";

            await _db.SaveData(sql, g);

            offsetX = 0;
            offsetY = 0;
            await OnInitializedAsync();
        }

        //
        void OpenDeletePopup(GameModel currGame)
        {
            // Open the popup prompting a delete
            ShowDeletePopup = true;
            selectedGame = currGame;
        }
        void CloseDeletePopup()
        {
            ShowDeletePopup = false;
            offsetX = 0;
            offsetY = 0;
        }

        private async Task DeleteGame(GameModel game)
        {
            ShowDeletePopup = false;

            GameModel g = new GameModel()
            {
                GameName = game.GameName,
                GameCoverUrl = game.GameCoverUrl,
                GameId = game.GameId
            };

            string sql = "delete from game where GameId = @GameId";
            await _db.SaveData(sql, g);

            offsetX = 0;
            offsetY = 0;

            await OnInitializedAsync();
        }

        private async Task UpdateGenre()
        {
            GameGenreModel gg = new GameGenreModel()
            {
                GameId = selectedGame.GameId,
                GenreId = genres.FirstOrDefault(x => x.GenreName == selectedGenre.GenreName).GenreId // Grab ID where name matches
            };

            string sql = @"insert into dbo.GameGenre (GameId, GenreId)" + 
                            "values(@GameId, @GenreId)";
            await _db3.SaveData<GameGenreModel>(sql, gg);

            await OnInitializedAsync();

            specificGenres.Clear(); // Clear up specific genres list, otherwise they pile up
            grabSpecificGenres(selectedGame); // Repopulate to display the new 

            selectedGenre = new GenreModel(); // Clear up the selected genre, otherwise it'll automatically display when the next game is edited
        }

    }
}
