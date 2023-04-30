using System.ComponentModel.DataAnnotations;

namespace UserAuth.Models.RequestsDTO
{
	public class LogOutRequestDTO
	{
		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Login { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		public string RefreshToken { get; set; } = string.Empty;
	}
}
