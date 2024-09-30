using System.ComponentModel.DataAnnotations;

namespace WebApiBasicAuth.Domain.Models;

// Entity to execise POST/PUT operations
public class WeatherSetupUpdate
{
    /// <summary>
    /// The first name of the author
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string SettingName { get; set; }

    /// <summary>
    /// The last name of the author
    /// </summary>         
    [Required]
    [MaxLength(150)]
    public string SettingValue { get; set; }
}
