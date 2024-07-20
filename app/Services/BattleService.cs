namespace DuelistApi.Services
{
  public class BattleService
  {
    private readonly CharacterService _characterService;

    public BattleService(CharacterService characterService)
    {
      _characterService = characterService;
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
      int round = 0;
      // We keep all battles under 100 rounds, a failsafe in case neither duelist perishes in combat
      while (characterOne.CurrentHealthPoints > 0 && characterTwo.CurrentHealthPoints > 0 && round < 100)
      {
        // calculate speed, turn order
        // calculate attack and apply damage
        round++;
      }

      _characterService.Save(characterOne);
      _characterService.Save(characterTwo);
      // return log
      return ""; //********************************************************************** fix me
    }
  }
}