namespace DuelistApi.Models
{
  public abstract class Job
  {
    public string Name { get; set; }
    public int HealthPoints { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intelligence { get; set; }
    public int AttackModifier { get; set; }
    public int SpeedModifier { get; set; }
    public abstract int CalculateAttackModifier();
    public abstract int CalculateSpeedModifier();
  }
}