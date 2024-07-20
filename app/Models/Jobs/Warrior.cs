using System;

namespace DuelistApi.Models
{
  public class Warrior : Job
  {
    public Warrior()
    {
      Name = "Warrior";
      HealthPoints = 20;
      Strength = 10;
      Dexterity = 5;
      Intelligence = 5;
      AttackModifier = CalculateAttackModifier();
      SpeedModifier = CalculateSpeedModifier();
    }

    // rounded to the nearest 32-bit signed integer
    // If value is halfway between two whole numbers (e.g. 5.5), the even number is returned (e.g. 6)
    public override int CalculateAttackModifier()
    {
        return Convert.ToInt32((Strength * 0.8) + (Dexterity * 0.2));
    }

    public override int CalculateSpeedModifier()
    {
        return Convert.ToInt32((Dexterity * 0.6) + (Intelligence * 0.2));
    }
  }
}