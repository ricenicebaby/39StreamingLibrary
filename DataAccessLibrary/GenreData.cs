using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class GenreData : IGenreData
    {
        private readonly ISqlDataAccess db;

        public GenreData(ISqlDataAccess db)
        {
            this.db = db;
        }

        public Task<List<GenreModel>> GetGenres()
        {
            string sql = "select GenreId, GenreName from dbo.Genre";
            return this.db.LoadData<GenreModel, dynamic>(sql, new { });
        }

        public Task InsertGenre(GenreModel genre)
        {
            string sql = @"insert into dbo.Genre (GenreName)
                            values (@GenreName);";

            return this.db.SaveData(sql, genre);
        }
    }
}
