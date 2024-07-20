using System;
using System.ComponentModel.DataAnnotations;
using DuelistApi.Models;
using DuelistApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuelistApi.Controllers
{
  [ApiController]
  public class BattleController : ControllerBase
  {
    private readonly BattleService _battleService;

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

    public BattleRequest(int one, int two)
    {
      CharacterOneId = one;
      CharacterTwoId = two;
    }
  }
}