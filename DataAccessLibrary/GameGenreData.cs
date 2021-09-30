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

        public Task<List<GameGenreModel>> LoadData<T, U>(string sql, U parameters)
        {
            return this.db.LoadData<GameGenreModel, dynamic>(sql, new { });
        }

        public Task SaveData<T>(string sql, T parameters)
        {
            return this.db.SaveData(sql, parameters);
        }
    }
}
