using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GameSupply.Models
{
    public partial class GameSupplyContext : DbContext
    {
        private static GameSupplyContext? _context = new GameSupplyContext();
        public static GameSupplyContext GetContext()
        {
            return _context;
        }

        public GameSupplyContext(DbContextOptions<GameSupplyContext> options)
            : base(options)
        {
        }
        public GameSupplyContext()
        {

        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<LoginSessionsHistory> LoginSessionsHistories { get; set; }
        public virtual DbSet<PriceRange> PriceRanges { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=GameSupply;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.IdGame);

                entity.Property(e => e.IdGame)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Game");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IdGenre).HasColumnName("ID_Genre");

                entity.Property(e => e.IdPublisher).HasColumnName("ID_Publisher");

                entity.Property(e => e.PreviewImage)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IdGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Genres");

                entity.HasOne(d => d.IdPublisherNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IdPublisher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Users");

                entity.Property(e => e.DownloadLink)
                    .HasColumnName("DownloadLink")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.IdGenre);

                entity.Property(e => e.IdGenre)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Genre");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LoginSessionsHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LoginSessionsHistory");

                entity.Property(e => e.IdLoginSession).HasColumnName("ID_LoginSession");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.UserStatus).HasMaxLength(50);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginSessionsHistory_Users");
            });

            modelBuilder.Entity<PriceRange>(entity =>
            {
                entity.HasKey(e => e.IdPriceRange);

                entity.Property(e => e.IdPriceRange)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_PriceRange");

                entity.Property(e => e.RangeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_User");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
