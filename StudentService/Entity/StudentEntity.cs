using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Entity;

[Table("students")]
public record StudentEntity
{
    public StudentEntity(string fullName, string email, string phone)
    {
        FullName = fullName;
        Email = email;
        Phone = phone;
        IsActive = true;
    }

    [Key]
    [Column("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [MaxLength(100)]
    [Column("full_name")]
    public string FullName { get; set; }
    
    [Required]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; }
    
    [Required]
    [Column("phone")]
    public string Phone { get; set; }

    [Column("is_active")] public bool IsActive { get; set; } = true;

    [Column("created_at")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}