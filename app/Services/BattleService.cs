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
      // string log = $"Battle between {characterOne.Name} ({characterOne.Job.Name}) - {characterOne.CurrentHealthPoints} HP and {characterTwo.Name} ({characterTwo.Job.Name}) - {characterTwo.CurrentHealthPoints} HP begins!\n";
      string log = BeginBattleMessage(characterOne, characterTwo);
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
            log += SpeedMessage(characterOne, speedOne, characterTwo, speedTwo);

            var damageOne = RollAndApplyDamage(characterOne, characterTwo);
            log += AttackMessage(characterOne, characterTwo, damageOne);
            if (characterTwo.CurrentHealthPoints == 0)
            {
              log += WinnerMessage(characterOne);
              break;
            }

            var damageTwo = RollAndApplyDamage(characterTwo, characterOne);
            log += AttackMessage(characterTwo, characterOne, damageTwo);
            if(characterOne.CurrentHealthPoints == 0)
            {
              log += WinnerMessage(characterTwo);
              break;
            }
          }
          else if (speedTwo > speedOne)
          {
            speedDecision = true;
            log += SpeedMessage(characterTwo,speedTwo, characterOne, speedOne);
            
            var damageTwo = RollAndApplyDamage(characterTwo, characterOne);
            log += AttackMessage(characterTwo,characterOne, damageTwo);
            if(characterOne.CurrentHealthPoints == 0)
            {
              log += WinnerMessage(characterTwo);
              break;
            }

            var damageOne = RollAndApplyDamage(characterOne, characterTwo);
            log += AttackMessage(characterOne, characterTwo, damageOne);
            if (characterTwo.CurrentHealthPoints == 0)
            {
              log += WinnerMessage(characterOne);
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

    // Normally because only the BattleService uses these they would be private, but rolling with these character stats is fun. 
    public int RollAndApplyDamage(Character characterOne, Character characterTwo)
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

    private string BeginBattleMessage(Character characterOne, Character characterTwo)
    {
      return $"Battle between {characterOne.Name} ({characterOne.Job.Name}) - {characterOne.CurrentHealthPoints} HP and {characterTwo.Name} ({characterTwo.Job.Name}) - {characterTwo.CurrentHealthPoints} HP begins!\n";
    }
    private string SpeedMessage(Character faster, int fasterSpeed, Character slower, int slowerSpeed)
    {
      return $"{faster.Name} {fasterSpeed} speed was faster than {slower.Name} {slowerSpeed} speed and will begin this round.\n";
    }

    private string AttackMessage(Character attacker, Character defender, int damage)
    {
      return $"{attacker.Name} attacks {defender.Name} for {damage} damage. {defender.Name} has {defender.CurrentHealthPoints} HP remaining.\n";
    }

    private string WinnerMessage(Character winner)
    {
     return $"{winner.Name} wins the battle! {winner.Name} still has {winner.CurrentHealthPoints} HP remaining!\n";
    }
  }
}