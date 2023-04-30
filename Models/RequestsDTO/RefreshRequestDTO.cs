using System.ComponentModel.DataAnnotations;

namespace UserAuth.Models.RequestsDTO
{
	public class RefreshRequestDTO
	{
		[Required(AllowEmptyStrings = false)]
		public string RefreshToken { get; set; } = string.Empty;
	}
}
