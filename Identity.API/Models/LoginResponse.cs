using System.ComponentModel.DataAnnotations;

namespace AluraChallenge.VideoSharingPlatform.Services.Identity.API.Models;

public class LoginResponse
{
    [Required]
    public required string AccessToken { get; set; }

    [Required]
    public required DateTime ExpiresIn { get; set; }
}
