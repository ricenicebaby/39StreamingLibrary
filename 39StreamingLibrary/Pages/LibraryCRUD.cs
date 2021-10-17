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
        private async Task CreateGame()
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

        private async Task UpdateGame()
        {
            GameModel g = new GameModel()
            {
                GameName = selectedGame.GameName,
                GameCoverUrl = selectedGame.GameCoverUrl,
                GameDescription = selectedGame.GameDescription,
                GameDate = selectedGame.GameDate,
                GameId = selectedGame.GameId
            };

            string sql = "update dbo.Game set GameName = @GameName where GameId = @GameId" +
                    " update dbo.Game set GameCoverUrl = @GameCoverUrl where GameId = @GameId" +
                @" update dbo.Game set GameDescription = @GameDescription where GameId = @GameId" +
               @" update dbo.Game set GameDate = @GameDate where GameId = @GameId";

            await _db.SaveData<GameModel>(sql, g);
            await OnInitializedAsync();
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

            string sql = "delete from dbo.Game where GameId = @GameId";
            await _db.SaveData(sql, g);

            await OnInitializedAsync();
        }

        // Genre: genres
        private async Task CreateGenre()
        {
            GenreModel g = new GenreModel
            {
                GenreName = newGenre.GenreName
            };

            string sql = @"insert into dbo.Genre (GenreName)" +
                    "values (@GenreName);";
            await _db2.SaveData<GenreModel>(sql, g);

            genres.Add(g);

            await OnInitializedAsync();

            newGenre = new DisplayGenreModel(); // Clear up the new game
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

        private async Task DeleteGameGenre(GenreModel deletedGenre)
        {
            GameGenreModel gg = new GameGenreModel()
            {
                GameId = selectedGame.GameId,
                GenreId = deletedGenre.GenreId
            };

            string sql = "delete from dbo.GameGenre where GameId = @GameId and GenreId = @GenreId";
            await _db3.SaveData(sql, gg);

            await OnInitializedAsync();
        }

        // GameVideo: Game and associated videos with it
        private async Task CreateGameVideo()
        {
            GameVideoModel gv = new GameVideoModel()
            {
                GameId = selectedGame.GameId,
                VideoUrl = selectedVideo.VideoUrl
            };

            string sql = @"insert into dbo.GameVideo (GameId, VideoUrl)" +
                            "values(@GameId, @VideoUrl)";
            await _db4.SaveData<GameVideoModel>(sql, gv);

            await OnInitializedAsync();

            specificVideos.Clear(); // Clear up specific videos list, otherwise they pile up
            grabSpecificVideos(selectedGame);

            selectedVideo = new GameVideoModel(); // Clear up the selected video, otherwise it'll automatically display when the next game is edited
        }

        private async Task UpdateGameVideo(GameVideoModel selectedVideo)
        {
            GameVideoModel g = new GameVideoModel()
            {
                GameVideoId = selectedVideo.GameVideoId,
                GameId = selectedVideo.GameId,
                VideoUrl = selectedVideo.VideoUrl,
                VideoDate = selectedVideo.VideoDate
            };

            string sql = "update dbo.GameVideo set VideoUrl = @VideoUrl where GameVideoId = @GameVideoId" +
                    " update dbo.GameVideo set VideoDate = @VideoDate where VideoUrl = @VideoUrl";

            await _db4.SaveData<GameVideoModel>(sql, g);
            await OnInitializedAsync();
        }

    }
}
