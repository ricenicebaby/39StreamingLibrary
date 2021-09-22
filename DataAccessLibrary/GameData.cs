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

        public Task<List<GameModel>> LoadData<T, U>(string sql, U parameters)
        {
            return this.db.LoadData<GameModel, dynamic>(sql, new { });
        }

        public Task SaveData<T>(string sql, T parameters)
        {
            return this.db.SaveData(sql, parameters);
        }

    }
}
