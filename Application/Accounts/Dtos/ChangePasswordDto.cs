using System.ComponentModel.DataAnnotations;

namespace Application.Accounts.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(ReNewPassword))]
        public string ReNewPassword { get; set; }
    }
}
