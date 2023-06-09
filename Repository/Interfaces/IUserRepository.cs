﻿using UserAuth.Models;

namespace UserAuth.Repository.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetUsersAsync();
		Task<User> GetUserAsync(int id);

		Task<User> RegisterUserAsync(User user);
		Task<User> LogInUserAsync(string login, string password);

		Task AddRefreshTokenAsync(RefreshToken refreshToken);
		Task<RefreshToken> GetRefreshTokenByToken(string token);
		Task RemoveOldRefreshToken(string token);
		Task RemoveUsersRefreshTokens(int userId);

		Task ChangePasswordAsync(string login, string oldPassword, string newPassword);
	}
}
