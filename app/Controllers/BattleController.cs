using System.ComponentModel.DataAnnotations;
using DuelistApi.Models;
using DuelistApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuelistApi.Controllers
{
  [ApiController]
  public class BattleController : ControllerBase
  {
    // [HttpPost]
    // [Route("[controller]")]
    // public string Battle([FromBody] BattleRequest request)
    // {

    // }
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