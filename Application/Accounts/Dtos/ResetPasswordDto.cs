using System.ComponentModel.DataAnnotations;

namespace Application.Accounts.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        public string UserName { get; set; }
        public string ResetPasswordToken { get; set; }

    }
}
