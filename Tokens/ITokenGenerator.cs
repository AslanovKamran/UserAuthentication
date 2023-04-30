using UserAuth.Models;

namespace UserAuth.Tokens
{
	public interface ITokenGenerator
	{
		string GenerateToken(User user);
		public string GenerateRefreshToken();
	}
}
