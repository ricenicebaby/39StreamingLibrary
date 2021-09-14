using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class GameGenreData : IGameGenreData
    {
        private readonly ISqlDataAccess db;

        public GameGenreData(ISqlDataAccess db)
        {
            this.db = db;
        }

        public Task<List<GameGenreModel>> GetGameGenres()
        {
            string sql = "select GameId, GenreId, GameIdString, GenreIdString from dbo.GameGenre";
            return this.db.LoadData<GameGenreModel, dynamic>(sql, new { });
        }
    }
}
