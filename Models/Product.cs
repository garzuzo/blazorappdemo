using System.ComponentModel;

namespace blazorappdemo;
public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string[] Images
    {
        get { return images; }
        set
        {
            images = value.Select(s => s.Replace("\"", "").Replace("[", "").Replace("]", "")).ToArray();
        }
    }
    private string[] images { get; set; }
    public string? Image { get; set; }
}