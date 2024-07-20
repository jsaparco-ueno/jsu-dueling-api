using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DuelistApi.Models;
using DuelistApi.Services;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Get(int id)
    {
      try
      {
        var character = _characterService.Get(id);
        
        return Ok(character);
      }
      catch (Exception e)
      {
        return BadRequest(new {
          error = e.Message
        });
      }
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

    public CharacterRequest(string name, string job)
    {
      Name = name;
      Job = job;
    }
  }
} 