using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DuelistApi.Models;
using DuelistApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuelistApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CharacterController: ControllerBase
  {
    private readonly CharacterService _characterService;

    public CharacterController(CharacterService characterService)
    {
      _characterService = characterService;
    }

    // If the character list gets very big we should consider paginating this response
    [HttpGet]
    public IEnumerable<Character> Get()
    {
      return _characterService.GetCharacters();
    }
  }
} 