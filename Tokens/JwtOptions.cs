using Microsoft.IdentityModel.Tokens;

namespace UserAuth.Tokens
{
	public class JwtOptions
	{
		public string Issuer { get; set; } = string.Empty;
		public string Audience { get; set; } = string.Empty;
		public TimeSpan AccessValidFor { get; set; }
		public TimeSpan RefreshValidFor { get; set; }
		public SigningCredentials? SigningCredentials { get; set; }
	}
}
