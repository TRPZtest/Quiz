using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuizApi.Data.Db.Enteties;

public partial class TestingDbContext : DbContext
{
    public TestingDbContext()
    {
    }

    public TestingDbContext(DbContextOptions<TestingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Take> Takes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-A8GKV9V;Database=testingDb;Trusted_Connection=Yes;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Options__3214EC07CF91034F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AnswerText)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Options__Questio__0B679CE2");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07053BE4E6");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.QuestionText)
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__TestI__04BA9F53");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quizzes__3214EC07B2F3E98D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(36)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Response__3214EC07C7FDE3E6");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Option).WithMany(p => p.Responses)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__Optio__0F382DC6");

            entity.HasOne(d => d.Question).WithMany(p => p.Responses)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__Quest__102C51FF");

            entity.HasOne(d => d.Take).WithMany(p => p.Responses)
                .HasForeignKey(d => d.TakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__TakeI__0E44098D");
        });

        modelBuilder.Entity<Take>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Takes__3214EC07ABF7FC71");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Test).WithMany(p => p.Takes)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__TestId__088B3037");

            entity.HasOne(d => d.User).WithMany(p => p.Takes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__UserId__07970BFE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC076E0705B1");

            entity.HasIndex(e => new { e.Login, e.Password }, "LoginPassword");

            entity.Property(e => e.Id).ValueGeneratedNever();
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
                        .HasConstraintName("FK__UsersToQu__QuizI__01DE32A8"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersToQu__UserI__00EA0E6F"),
                    j =>
                    {
                        j.HasKey("UserId", "QuizId").HasName("PK__UsersToQ__EF3CE6A49E03DEE7");
                        j.ToTable("UsersToQuizes");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
