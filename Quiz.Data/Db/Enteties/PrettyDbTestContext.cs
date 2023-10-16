using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuizApi.Data.Db.Enteties;

public partial class PrettyDbTestContext : DbContext
{
    public PrettyDbTestContext()
    {
    }

    public PrettyDbTestContext(DbContextOptions<PrettyDbTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Take> Takes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:prettytestdb.database.windows.net,1433;Initial Catalog=PrettyDbTest;Persist Security Info=False;User ID=adminTest;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Options__3214EC07DEF1DCFE");

            entity.Property(e => e.AnswerText)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Options__Questio__47C69FAC");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC0722615378");

            entity.Property(e => e.QuestionText)
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__TestI__4119A21D");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quizzes__3214EC0746D01074");

            entity.Property(e => e.Name)
                .HasMaxLength(36)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Response__3214EC0730778FE3");

            entity.Property(e => e.Created).HasColumnType("datetime");

            entity.HasOne(d => d.Option).WithMany(p => p.Responses)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__Optio__4B973090");

            entity.HasOne(d => d.Take).WithMany(p => p.Responses)
                .HasForeignKey(d => d.TakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__TakeI__4AA30C57");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Results__3214EC070591D4D1");

            entity.HasIndex(e => e.TakeId, "UQ__Results__AC0C21A139C1BB0C").IsUnique();

            entity.HasOne(d => d.Take).WithOne(p => p.Result)
                .HasForeignKey<Result>(d => d.TakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__TakeId__4F67C174");
        });

        modelBuilder.Entity<Take>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Takes__3214EC0738190571");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Takes)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__QuizId__44EA3301");

            entity.HasOne(d => d.User).WithMany(p => p.Takes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__UserId__43F60EC8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07FCA218A5");

            entity.HasIndex(e => new { e.Login, e.Password }, "LoginPassword").IsUnique();

            entity.Property(e => e.Login)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasMany(d => d.Quizzes).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersToQuize",
                    r => r.HasOne<Quiz>().WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersToQu__QuizI__3E3D3572"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersToQu__UserI__3D491139"),
                    j =>
                    {
                        j.HasKey("UserId", "QuizId").HasName("PK__UsersToQ__EF3CE6A41DAA911B");
                        j.ToTable("UsersToQuizes");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
