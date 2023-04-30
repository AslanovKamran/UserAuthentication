namespace UserAuth.Models
{
	public class UserTokens
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }

		public UserTokens(string accessToken, string refreshToken)
		{
			AccessToken = accessToken;
			RefreshToken = refreshToken;
		}
	}
}
