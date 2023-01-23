using System.ComponentModel.DataAnnotations;

namespace Indigo_Travel.Areas.Manage.ViewModles;
public class AdminLoginViewModel
{
    [Required]
    [StringLength(maximumLength: 30)]
    public string UserName { get; set; }

    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

