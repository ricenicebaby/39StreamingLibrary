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
    public class GameVideoData : IGameVideoData
    {
        private readonly ISqlDataAccess db;
        public GameVideoData(ISqlDataAccess db)
        {
            this.db = db;
        }

        public Task<List<GameVideoModel>> LoadData<T, U>(string sql, U parameters)
        {
            return this.db.LoadData<GameVideoModel, dynamic>(sql, new { });
        }

        public Task SaveData<T>(string sql, T parameters)
        {
            return this.db.SaveData(sql, parameters);
        }

    }
}
