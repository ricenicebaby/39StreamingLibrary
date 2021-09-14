using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IGameGenreData
    {
        Task<List<GameGenreModel>> GetGameGenres();
    }
}