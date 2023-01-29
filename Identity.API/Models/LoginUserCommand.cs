using System.ComponentModel.DataAnnotations;

namespace AluraChallenge.VideoSharingPlatform.Services.Identity.API.Models;

public class LoginUserCommand
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
