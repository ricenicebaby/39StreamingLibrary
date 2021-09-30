using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IGameGenreData
    {
        Task<List<GameGenreModel>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);

    }
}