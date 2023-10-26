using System.Text;
using ForumApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Data
{
    public class ForumDbContext : DbContext
    {
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Forum> Forums { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Ban> Bans { get; set; } = null!;

        public ForumDbContext(DbContextOptions<ForumDbContext> options) 
            : base(options)
        {}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>(a => 
            {
                a.HasKey(a => a.Id);

                a.HasIndex(a => a.LoginName).IsUnique();
                a.HasIndex(a => a.Email).IsUnique();

                a.Property(a => a.Username)
                    .IsRequired();
                a.Property(a => a.LoginName)
                    .IsRequired();
                a.Property(a => a.Email)
                    .IsRequired();
                a.Property(a => a.PasswordHash)
                    .IsRequired();
                a.Property(a => a.LastLoggedAt)
                    .HasDefaultValueSql("timezone('utc', now())");

                a.Property(a => a.DeletedAt)
                    .HasDefaultValue(null);
            });

            builder.Entity<Token>(t => 
            {
                t.HasKey(t => t.Id);

                t.HasIndex(t => t.RefreshToken).IsUnique();

                t.Property(t => t.RefreshToken)
                    .IsRequired();
                t.Property(t => t.ExpiresAt)
                    .IsRequired();

                t.HasOne(t => t.Account)
                    .WithMany(a => a.Tokens)
                    .HasForeignKey(t => t.AccountId);
            });

            builder.Entity<Section>(s => 
            {
                s.HasKey(s => s.Id);

                s.Property(s => s.Title)
                    .IsRequired();

                s.Property(s => s.IsHidden)
                    .HasDefaultValue(false);
            });

            builder.Entity<Forum>(f => 
            {
                f.HasKey(f => f.Id);

                f.Property(f => f.Title)
                    .IsRequired();

                f.Property(f => f.IsClosed)
                    .HasDefaultValue(false);

                f.Property(f => f.DeletedAt)
                    .HasDefaultValue(null);

                f.HasOne(f => f.Section)
                    .WithMany(s => s.Forums)
                    .HasForeignKey(f => f.SectionId);
            });

            builder.Entity<Topic>(t => 
            {
                t.HasKey(t => t.Id);

                t.Property(t => t.Title)
                    .IsRequired();

                t.Property(t => t.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("timezone('utc', now())");

                t.Property(t => t.IsClosed)
                    .HasDefaultValue(false);

                t.Property(t => t.IsPinned)
                    .HasDefaultValue(false);

                t.Property(t => t.DeletedAt)
                    .HasDefaultValue(null); 

                t.HasOne(t => t.Author)
                    .WithMany(a => a.Topics)
                    .HasForeignKey(t => t.AccountId)
                    .OnDelete(DeleteBehavior.NoAction);

                t.HasOne(t => t.Forum)
                    .WithMany(f => f.Topics)
                    .HasForeignKey(t => t.ForumId);
            });

            builder.Entity<Post>(p => 
            {
                p.HasKey(p => p.Id);

                p.Property(p => p.Content)
                    .IsRequired();

                p.Property(p => p.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("timezone('utc', now())");

                p.Property(p => p.DeletedAt)
                    .HasDefaultValue(null);

                p.HasOne(p => p.Topic)
                    .WithMany(t => t.Posts)
                    .HasForeignKey(p => p.TopicId);

                p.HasOne(p => p.Author)
                    .WithMany(a => a.Posts)
                    .HasForeignKey(p => p.AccountId);
            });

            builder.Entity<Ban>(b => {
                b.HasKey(b => b.Id);

                b.Property(b => b.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("timezone('utc', now())");

                b.Property(b => b.ExpiresAt)
                    .IsRequired();

                b.Property(b => b.Reason)
                    .IsRequired();

                b.Property(b => b.IsPermanent)
                    .HasDefaultValue(false);

                b.Property(b => b.IsActive)
                    .HasDefaultValue(true);

                b.HasOne(b => b.Account)
                    .WithMany(a => a.RecievedBans)
                    .HasForeignKey(b => b.AccountId);

                b.HasOne(b => b.Moderator)
                    .WithMany(a => a.GivenBans)
                    .HasForeignKey(b => b.ModeratorId);
            });

            base.OnModelCreating(builder);
        }
    }
}