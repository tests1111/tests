using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EmailService;

public class EmailMessage
{
    [Required]
    [EmailAddress]
    [MaxLength(30)]
    public string From { set; get; } = default!;

    [Required]
    [EmailAddress]
    [MaxLength(30)]
    public string To { set; get; } = default!;

    [Required]
    [MaxLength(30)]
    public string Subject { set; get; } = default!;

    [Required]
    [MaxLength(500)]
    public string Body { set; get; } = default!;
}