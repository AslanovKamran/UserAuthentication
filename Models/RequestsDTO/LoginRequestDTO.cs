using System.ComponentModel.DataAnnotations;

namespace UserAuth.Models.RequestsDTO
{
	public class LoginRequestDTO
	{
		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Login { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Password { get; set; } = string.Empty;
	}
}
