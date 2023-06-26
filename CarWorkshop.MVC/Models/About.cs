namespace CarWorkshop.MVC.Models;
public class About
{
    public string? Title { get; set; }
    public string? Descritpion { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}
