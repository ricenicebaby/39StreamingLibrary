using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IGenreData
    {
        Task<List<GenreModel>> GetGenres();
        Task InsertGenre(GenreModel genre);
    }
}