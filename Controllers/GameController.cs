using Microsoft.AspNetCore.Mvc;
using SpecificationPatternInEfCore.Service;
using SpecificationPatternInEfCore.Specifications;

namespace SpecificationPatternInEfCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames()
    {
        var result = await _gameService.GetAllGames();
        return Ok(result);
    }

    [HttpGet]
    [Route("game-specification")]
    public async Task<IActionResult> GetAllGamesWithSpecification()
    {
        var result = await _gameService.GetAllGamesAsync(new GameSpecification());
        return Ok(result);
    }   
}
