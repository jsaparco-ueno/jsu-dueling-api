using System;
using System.ComponentModel.DataAnnotations;
using DuelistApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuelistApi.Controllers
{
  [ApiController]
  public class BattleController : ControllerBase
  {
    private readonly BattleService _battleService;

    public BattleController(BattleService battleService)
    {
      _battleService = battleService;
    }

    [HttpPost]
    [Route("[controller]")]
    public IActionResult Battle([FromBody] BattleRequest request)
    {
      try
      {
        var battleLog = _battleService.Battle(request.CharacterOneId, request.CharacterTwoId);

        return Ok(battleLog);
      }
      catch (Exception e)
      {
        return BadRequest(new {
          error = e.Message
        });
      }
    }
  }

  public class BattleRequest
  {
    [Required]
    public int CharacterOneId { get; set; }
    [Required]
    public int CharacterTwoId { get; set; }

    public BattleRequest(int characterOneId, int characterTwoId)
    {
      CharacterOneId = characterOneId;
      CharacterTwoId = characterTwoId;
    }
  }
}