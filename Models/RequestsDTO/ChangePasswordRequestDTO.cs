using System.ComponentModel.DataAnnotations;

namespace UserAuth.Models.RequestsDTO
{
	public class ChangePasswordRequestDTO
	{
		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Login { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string OldPassword { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string NewPassword { get; set; } = string.Empty;
	}
}
