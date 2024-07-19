  namespace DuelistApi.Models
{
  public abstract class Character
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Job Job { get; set; }
  }
}