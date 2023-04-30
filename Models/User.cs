namespace UserAuth.Models;

public partial class User
{
	public int Id { get; set; }

	public string Login { get; set; } = null!;

	public string Password { get; set; } = null!;

	public int RoleId { get; set; }

	public string Salt { get; set; } = null!;

	public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

	public virtual Role Role { get; set; } = null!;
}
