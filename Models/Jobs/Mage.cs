using System;

namespace DuelistApi.Models
{
  public class Mage : Job
  {
    public Mage()
    {
      Name = "Mage";
      HealthPoints = 12;
      Strength = 5;
      Dexterity = 6;
      Intelligence = 10;
    }

    // rounded to the nearest 32-bit signed integer
    // If value is halfway between two whole numbers (e.g. 5.5), the even number is returned (e.g. 6)
    public override int AttackModifier()
    {
        return Convert.ToInt32((Strength * 0.20) + (Dexterity * 0.20) + (Intelligence * 1.20));
    }

    public override int SpeedModifier()
    {
        return Convert.ToInt32((Dexterity * 0.4) + (Strength * .1));
    }
  }
}