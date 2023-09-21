using ForumApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Data
{
    public class ForumDbContext : DbContext
    {
        public virtual DbSet<Token> Tokens {get;set;} = null!;
        public virtual DbSet<Account> Accounts {get;set;} = null!;
        public virtual DbSet<Section> Sections {get;set;} = null!;
        public virtual DbSet<Forum> Forums {get;set;} = null!;
        public virtual DbSet<Topic> Topics {get;set;} = null!;
        public virtual DbSet<Post> Posts {get;set;} = null!;

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
                    .HasColumnType("nvarchar(32)")
                    .IsRequired();
                a.Property(a => a.LoginName)
                    .HasColumnType("nvarchar(32)")
                    .IsRequired();
                a.Property(a => a.Email)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired();
                a.Property(a => a.PasswordHash)
                    .IsRequired();

                a.Property(a => a.DeletedAt)
                    .HasDefaultValue(null);
            });

            builder.Entity<Token>(t => 
            {
                t.HasKey(t => t.Id);

                t.HasIndex(t => t.RefreshToken).IsUnique();

                t.Property(t => t.RefreshToken)
                    .HasColumnType("nvarchar(1024)")
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
                    .IsRequired()
                    .HasColumnType("nvarchar(256)");

                s.Property(s => s.IsHidden)
                    .HasDefaultValue(false);
            });

            builder.Entity<Forum>(f => 
            {
                f.HasKey(f => f.Id);

                f.Property(f => f.Title)
                    .IsRequired()
                    .HasColumnType("nvarchar(256)");

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
                    .IsRequired()
                    .HasColumnType("nvarchar(256)");

                t.Property(t => t.CreatedAt)
                    .IsRequired();

                t.Property(t => t.IsClosed)
                    .HasDefaultValue(false);

                t.Property(t => t.DeletedAt)
                    .HasDefaultValue(null); 

                t.HasOne(t => t.Author)
                    .WithMany(a => a.Topics)
                    .HasForeignKey(t => t.AccountId);

                t.HasOne(t => t.Forum)
                    .WithMany(f => f.Topics)
                    .HasForeignKey(t => t.ForumId);
            });

            builder.Entity<Post>(p => 
            {
                p.HasKey(p => p.Id);

                p.Property(p => p.Content)
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)");

                p.Property(p => p.CreatedAt)
                    .IsRequired();

                p.Property(p => p.DeletedAt)
                    .HasDefaultValue(null);

                p.HasOne(p => p.Topic)
                    .WithMany(t => t.Posts)
                    .HasForeignKey(p => p.TopicId);

                p.HasOne(p => p.Account)
                    .WithMany(a => a.Posts)
                    .HasForeignKey(p => p.AccountId);
            });

            base.OnModelCreating(builder);
        }
    }
}