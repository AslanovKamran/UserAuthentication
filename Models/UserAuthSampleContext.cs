using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserAuth.Models;

public partial class UserAuthSampleContext : DbContext
{
    public UserAuthSampleContext()
    {
    }

    public UserAuthSampleContext(DbContextOptions<UserAuthSampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC07DED42956");

            entity.Property(e => e.Expires).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(255);

			entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__RefreshTo__UserI__2B3F6F97");
		});

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07ACF8E703");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F60B5D936E").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07C4A9BA8C");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B73816707").IsUnique();

            entity.HasIndex(e => e.Salt, "UQ__Users__A152BCCEE8E19AC5").IsUnique();

            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Salt).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__286302EC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
