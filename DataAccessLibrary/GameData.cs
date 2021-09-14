using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

using System.Data.SqlClient;

namespace DataAccessLibrary
{
    public class GameData : IGameData
    {
        private readonly ISqlDataAccess db;

        public GameData(ISqlDataAccess db)
        {
            this.db = db;
        }

        public Task<List<GameModel>> GetGames()
        {
            string sql = "select GameId, GameName, GameCoverUrl, GameIdString from dbo.Game";
            return this.db.LoadData<GameModel, dynamic>(sql, new { });
        }

        public Task InsertGame(GameModel game)
        {
            string sql = @"insert into dbo.Game (GameName, GameCoverUrl)" +
                            "values (@GameName, @GameCoverUrl);";

            return this.db.SaveData(sql, game);
        }

    }
}
