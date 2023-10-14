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

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Take> Takes { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#pragma warning disable CS1030 // #warning directive
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-A8GKV9V;Database=testingDb;Trusted_Connection=Yes;TrustServerCertificate=True;");
//#pragma warning restore CS1030 // #warning directive

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Options__3214EC07FE07CE22");

            entity.Property(e => e.AnswerText)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Options__Questio__774B959C");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07B041944D");

            entity.Property(e => e.QuestionText)
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__TestI__709E980D");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quizzes__3214EC07808B17F0");

            entity.Property(e => e.Name)
                .HasMaxLength(36)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Response__3214EC07F0A2B57B");

            entity.HasIndex(e => new { e.TakeId, e.QuestionId }, "TakeIdOptionIdUnique").IsUnique();

            //entity.Property(e => e.Created).HasColumnType("datetime");

            entity.HasOne(d => d.Option).WithMany(p => p.Responses)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__Optio__7B1C2680");

            entity.HasOne(d => d.Question).WithMany(p => p.Responses)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__Quest__7C104AB9");

            entity.HasOne(d => d.Take).WithMany(p => p.Responses)
                .HasForeignKey(d => d.TakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__TakeI__7A280247");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Results__3214EC07D1CF6A42");

            entity.HasIndex(e => e.TakeId, "UQ__Results__AC0C21A151A1B0E8").IsUnique();

            entity.HasOne(d => d.Take).WithOne(p => p.Result)
                .HasForeignKey<Result>(d => d.TakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__TakeId__7FE0DB9D");
        });

        modelBuilder.Entity<Take>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Takes__3214EC07297E6C5C");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Takes)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__QuizId__746F28F1");

            entity.HasOne(d => d.User).WithMany(p => p.Takes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__UserId__737B04B8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07DAC1FE9D");

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
                        .HasConstraintName("FK__UsersToQu__QuizI__6DC22B62"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersToQu__UserI__6CCE0729"),
                    j =>
                    {
                        j.HasKey("UserId", "QuizId").HasName("PK__UsersToQ__EF3CE6A485AA9F41");
                        j.ToTable("UsersToQuizes");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
