using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IGenreData
    {
        Task<List<GenreModel>> LoadData<T, U>(string sql, U parameters);
        Task InsertGenre(GenreModel genre);
    }
}