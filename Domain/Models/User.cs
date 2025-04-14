using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
//using Domain.Entities;

namespace Domain.Models
{
    public class User
    {
        public User()
        {
            RefreshTokens = new List<RefreshToken>();
            CreatedAt = DateTime.UtcNow;
            Created = DateTime.UtcNow;
        }

        // Основные свойства пользователя
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Навигационные свойства
        public virtual ICollection<Complaint> Complaints { get; } = new List<Complaint>();
        public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
        public virtual ICollection<Order> Orders { get; } = new List<Order>();
        public virtual ICollection<ProviderService> ProviderServices { get; } = new List<ProviderService>();
        public virtual ICollection<Review> Reviews { get; } = new List<Review>();
        public virtual Roles? Roles { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
        public virtual ICollection<Tour> TourGuides { get; } = new List<Tour>();
        public virtual ICollection<Tour> TourProviders { get; } = new List<Tour>();

        // Свойства, связанные с аутентификацией
        public bool AcceptTerms { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }


        // Методы для интеграции с IdentityServer4
        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Exists(x => x.Token == token) ?? false;
        }

        public void AddRefreshToken(string token, double daysToExpire = 5)
        {
            RefreshTokens.Add(new RefreshToken
            {
                Token = token,
                Expires = DateTime.UtcNow.AddDays(daysToExpire),
                Created = DateTime.UtcNow
            });
        }

        public void RemoveRefreshToken(string token)
        {
            RefreshTokens.RemoveAll(x => x.Token == token);
        }

        public void RemoveOldRefreshTokens()
        {
            RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(2) <= DateTime.UtcNow);
        }

        public RefreshToken? GetRefreshToken(string token)
        {
            return RefreshTokens.FirstOrDefault(x => x.Token == token);
        }
    }
}