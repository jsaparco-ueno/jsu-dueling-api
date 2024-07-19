using System;

namespace DuelistApi.Models
{
  public class Thief : Job
  {
    public Thief()
    {
      Name = "Thief";
      HealthPoints = 15;
      Strength = 4;
      Dexterity = 10;
      Intelligence = 4;
    }

    // rounded to the nearest 32-bit signed integer
    // If value is halfway between two whole numbers (e.g. 5.5), the even number is returned (e.g. 6)
    public override int AttackModifier()
    {
        return Convert.ToInt32((Strength * 0.25) + Dexterity + (Intelligence * 0.25));
    }

    public override int SpeedModifier()
    {
        return Convert.ToInt32(Dexterity * 0.8);
    }
  }
}