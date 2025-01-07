

using System.ComponentModel.DataAnnotations;

namespace project.Data.Models.Domain;

public class Allowance
{
    public int Id { get; set; }
    [Required]
    public string? Allowance_Name { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
}

