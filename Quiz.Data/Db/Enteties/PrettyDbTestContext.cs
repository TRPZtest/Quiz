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
            entity.HasKey(e => e.Id).HasName("PK__Options__3214EC077C626EF2");

            entity.Property(e => e.AnswerText)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Options__Questio__05E3CDB6");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC075350A4E3");

            entity.Property(e => e.QuestionText)
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__TestI__7E42ABEE");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quizzes__3214EC075181DD8D");

            entity.Property(e => e.Name)
                .HasMaxLength(36)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Response__3214EC074FD55BEF");

            entity.HasOne(d => d.Option).WithMany(p => p.Responses)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__Optio__09B45E9A");

            entity.HasOne(d => d.Take).WithMany(p => p.Responses)
                .HasForeignKey(d => d.TakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Responses__TakeI__08C03A61");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Results__3214EC07021F5DAD");

            entity.HasIndex(e => e.TakeId, "UQ__Results__AC0C21A1D4D5A584").IsUnique();

            entity.HasOne(d => d.Take).WithOne(p => p.Result)
                .HasForeignKey<Result>(d => d.TakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__TakeId__0D84EF7E");
        });

        modelBuilder.Entity<Take>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Takes__3214EC0735577976");

            entity.HasIndex(e => new { e.UserId, e.QuizId }, "Take").IsUnique();

            entity.HasOne(d => d.Quiz).WithMany(p => p.Takes)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__QuizId__0307610B");

            entity.HasOne(d => d.User).WithMany(p => p.Takes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Takes__UserId__02133CD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07C250DBD1");

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
                        .HasConstraintName("FK__UsersToQu__QuizI__7B663F43"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersToQu__UserI__7A721B0A"),
                    j =>
                    {
                        j.HasKey("UserId", "QuizId").HasName("PK__UsersToQ__EF3CE6A406776264");
                        j.ToTable("UsersToQuizes");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
