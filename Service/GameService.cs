using Microsoft.EntityFrameworkCore;
using SpecificationPatternInEfCore.Entity;
using SpecificationPatternInEfCore.Persistent;
using SpecificationPatternInEfCore.Specifications;

namespace SpecificationPatternInEfCore.Service;

public interface IGameService
{
    Task<List<Game>> GetAllGames();
    Task<List<Game>> GetAllGamesAsync(Specification<Game> specification);
}
public class GameService : IGameService
{
    private readonly AppDbContext _appDbContext;

    public GameService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<List<Game>> GetAllGames()
    {
        var result = await _appDbContext
                .Game
                .Include(x => x.Genre)
                .ToListAsync();
        return result;
    }

    public async Task<List<Game>> GetAllGamesAsync(Specification<Game> specification)
    {
        return await SpecificationBuilder.GetQuery(_appDbContext.Game, specification).ToListAsync();
    }
}
