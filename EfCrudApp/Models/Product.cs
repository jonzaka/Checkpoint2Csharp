using System.ComponentModel.DataAnnotations;

namespace EfCrudApp.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
