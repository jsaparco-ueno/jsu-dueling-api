  namespace DuelistApi.Models
{
  public class Character
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int CurrentHealthPoints { get; set; }
    public Job Job { get; set; }
  }
}