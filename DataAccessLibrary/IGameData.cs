using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IGameData
    {
        Task<List<GameModel>> GetGames();
        Task InsertGame(GameModel game);
    }
}