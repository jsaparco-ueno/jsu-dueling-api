using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using DuelistApi.Models;
using DuelistApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace DuelistApi.Controllers
{
  [ApiController]
  public class CharacterController: ControllerBase
  {
    private readonly CharacterService _characterService;

    public CharacterController(CharacterService characterService)
    {
      _characterService = characterService;
    }

    // If the character list gets very big we should consider paginating this response
    [HttpGet]
    [Route("[controller]/get")]
    public IEnumerable<Character> Get()
    {
      return _characterService.GetCharacters();
    }

    [HttpGet]
    [Route("[controller]/get/{id}")]
    public Character Get(int id)
    {
      return _characterService.Get(id);
    }

    [HttpPost]
    [Route("[controller]/create")]
    public IActionResult Create([FromBody] CharacterRequest request)
    {
      Character character;
      try
      {
        character = _characterService.Create(request.Name, request.Job);

        return Ok(character);
      }
      catch (Exception e)
      {
        return BadRequest(new {
          error = e.Message
        });
      }
    }
  }

  public class CharacterRequest
  {
    [Required]
    public string Name { get; set;}
    [Required]
    public string Job { get; set; }
  }
} 