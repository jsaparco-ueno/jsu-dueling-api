using System;
using DuelistApi.Models;

namespace DuelistApi.Services
{
  public class BattleService
  {
    private readonly CharacterService _characterService;

    private readonly Random _randomNumber;

    public BattleService(CharacterService characterService)
    {
      _characterService = characterService;
      _randomNumber = new Random();
    }

    public string Battle(int characterIdOne, int characterIdTwo)
    {
      _characterService.ValidateCharacterId(characterIdOne);
      _characterService.ValidateCharacterId(characterIdTwo);
      // fetch characters
      var characterOne = _characterService.Get(characterIdOne);
      var characterTwo = _characterService.Get(characterIdTwo);
      // validate characters
      _characterService.ValidateCharacter(characterOne);
      _characterService.ValidateCharacter(characterTwo);
      
      // battle loop, generating the log
      string log = $"Battle between {characterOne.Name} ({characterOne.Job.Name}) - {characterOne.CurrentHealthPoints} HP and {characterTwo.Name} ({characterTwo.Job.Name}) - {characterTwo.CurrentHealthPoints} HP begins!\n";
      int round = 0;
      int speedRound = 0;
      bool speedDecision = false;
      // We keep all battles under 100 rounds, a failsafe in case neither duelist perishes in combat
      while (characterOne.CurrentHealthPoints > 0 && characterTwo.CurrentHealthPoints > 0 && round < 100)
      {
        // calculate speed, turn order
        while (!speedDecision && speedRound < 100)
        {
          var speedOne = Roll(characterOne.Job.SpeedModifier);
          var speedTwo = Roll(characterTwo.Job.SpeedModifier);

          if (speedOne > speedTwo)
          {
            speedDecision = true;
            log += $"{characterOne.Name} {speedOne} speed was faster than {characterTwo.Name} {speedTwo} speed and will begin this round.\n";
            var damageOne = RollAndApplyDamage(characterOne, characterTwo);
            log += $"{characterOne.Name} attacks {characterTwo.Name} for {damageOne} damage. {characterTwo.Name} has {characterTwo.CurrentHealthPoints} HP remaining.\n";
            if (characterTwo.CurrentHealthPoints == 0)
            {
              log += $"{characterOne.Name} wins the battle! {characterOne.Name} still has {characterOne.CurrentHealthPoints} HP remaining!\n";
              break;
            }

            var damageTwo = RollAndApplyDamage(characterTwo, characterOne);
            log += $"{characterTwo.Name} attacks {characterOne.Name} for {damageTwo} damage. {characterOne.Name} has {characterOne.CurrentHealthPoints} HP remaining.\n";
            if(characterOne.CurrentHealthPoints == 0)
            {
              log += $"{characterTwo.Name} wins the battle! {characterTwo.Name} still has {characterTwo.CurrentHealthPoints} HP remaining!\n";
              break;
            }
          }
          else if (speedTwo > speedOne)
          {
            speedDecision = true;
            log += $"{characterTwo.Name} {speedTwo} speed was faster than {characterOne.Name} {speedOne} speed and will begin this round.\n";
            var damageTwo = RollAndApplyDamage(characterTwo, characterOne);
            log += $"{characterTwo.Name} attacks {characterOne.Name} for {damageTwo} damage. {characterOne.Name} has {characterOne.CurrentHealthPoints} HP remaining.\n";
            if(characterOne.CurrentHealthPoints == 0)
            {
              log += $"{characterTwo.Name} wins the battle! {characterTwo.Name} still has {characterTwo.CurrentHealthPoints} HP remaining!\n";
              break;
            }

            var damageOne = RollAndApplyDamage(characterOne, characterTwo);
            log += $"{characterOne.Name} attacks {characterTwo.Name} for {damageOne} damage. {characterTwo.Name} has {characterTwo.CurrentHealthPoints} HP remaining.\n";
            if (characterTwo.CurrentHealthPoints == 0)
            {
              log += $"{characterOne.Name} wins the battle! {characterOne.Name} still has {characterOne.CurrentHealthPoints} HP remaining!\n";
              break;
            }
          }

          speedRound++;
        }
        speedDecision = false;
        speedRound = 0;
        round++;
      }

      _characterService.Save(characterOne);
      _characterService.Save(characterTwo);

      return log;
    }

    int RollAndApplyDamage(Character characterOne, Character characterTwo)
    {
        var damageOne = Roll(characterOne.Job.AttackModifier);
        characterTwo.CurrentHealthPoints -= damageOne;
        if (characterTwo.CurrentHealthPoints <= 0)
        {
            characterTwo.CurrentHealthPoints = 0;
        }

        return damageOne;
    }

    public int Roll(int max)
    {
      return _randomNumber.Next(0, max);
    }
  }
}