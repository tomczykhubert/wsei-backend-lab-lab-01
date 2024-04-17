using Infrastructure.EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Infrastructure;

public class QuizDbContext : IdentityDbContext<UserEntity, UserRole, int>
{
    public DbSet<QuizEntity> Quizzes { get; set; }
    public DbSet<QuizItemEntity> QuizItems { get; set; }
    public DbSet<QuizItemUserAnswerEntity> UserAnswers { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<QuizItemAnswerEntity> Answers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "DATA SOURCE=localhost;DATABASE=quiz_database;Integrated Security=true;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<QuizItemUserAnswerEntity>()
            .HasOne(e => e.QuizItem);
        
         modelBuilder.Entity<QuizItemUserAnswerEntity>()
            .HasOne<QuizEntity>()
            .WithMany()
            .HasForeignKey(a => a.QuizId);
        
        modelBuilder.Entity<QuizItemUserAnswerEntity>()
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<QuizItemAnswerEntity>()
            .HasData(
                new QuizItemAnswerEntity() {Id = 1, Answer = "1"},
                new QuizItemAnswerEntity() {Id = 2, Answer = "2"},
                new QuizItemAnswerEntity() {Id = 3, Answer = "3"},
                new QuizItemAnswerEntity() {Id = 4, Answer = "4"},
                new QuizItemAnswerEntity() {Id = 5, Answer = "5"},
                new QuizItemAnswerEntity() {Id = 6, Answer = "6"},
                new QuizItemAnswerEntity() {Id = 7, Answer = "7"},
                new QuizItemAnswerEntity() {Id = 8, Answer = "8"},
                new QuizItemAnswerEntity() {Id = 9, Answer = "9"},
                new QuizItemAnswerEntity() {Id = 10, Answer = "10"},
                new QuizItemAnswerEntity() {Id = 11, Answer = "11"},
                new QuizItemAnswerEntity() {Id = 12, Answer = "12"},
                new QuizItemAnswerEntity() {Id = 13, Answer = "13"},
                new QuizItemAnswerEntity() {Id = 14, Answer = "14"},
                new QuizItemAnswerEntity() {Id = 15, Answer = "15"}
                );
            
        modelBuilder.Entity<QuizItemEntity>()
            .HasData(
                new QuizItemEntity()
                {
                    Id = 1,
                    Question = "2 + 3",
                    CorrectAnswer = "5"
                },
                new QuizItemEntity()
                {
                    Id = 2,
                    Question = "2 * 3",
                    CorrectAnswer = "6"
                },
                new QuizItemEntity()
                {
                    Id = 3,
                    Question = "8 - 3",
                    CorrectAnswer = "5"
                },
                new QuizItemEntity()
                {
                    Id = 4,
                    Question = "8 : 2",
                    CorrectAnswer = "4"
                },
                new QuizItemEntity()
                {
                    Id = 5,
                    Question = "7 * 3",
                    CorrectAnswer = "21"
                },
                new QuizItemEntity()
                {
                    Id = 6,
                    Question = "2 : 2",
                    CorrectAnswer = "1"
                },
                new QuizItemEntity()
                {
                    Id = 7,
                    Question = "25 * 5",
                    CorrectAnswer = "125"
                },
                new QuizItemEntity()
                {
                    Id = 8,
                    Question = "43 * 2",
                    CorrectAnswer = "86"
                }
            );
            
        modelBuilder.Entity<QuizEntity>()
            .HasData(
                new QuizEntity()
                {
                    Title = "Matematyka",
                    Id = 1
                },
                new QuizEntity()
                {
                    Title = "Arytmetyka",
                    Id = 2
                }
            );
            
        modelBuilder.Entity<QuizEntity>()
            .HasMany<QuizItemEntity>(q => q.Items)
            .WithMany(e => e.Quizzes)
            .UsingEntity(
                join => join.HasData(
                    new {QuizzesId = 1, ItemsId = 1},
                    new {QuizzesId = 1, ItemsId = 2},
                    new {QuizzesId = 1, ItemsId = 3},
                    new {QuizzesId = 2, ItemsId = 5},
                    new {QuizzesId = 2, ItemsId = 6},
                    new {QuizzesId = 2, ItemsId = 7}
                )
            );

        modelBuilder.Entity<QuizItemEntity>()
            .HasMany<QuizItemAnswerEntity>(q => q.IncorrectAnswers)
            .WithMany(e => e.QuizItems)
            .UsingEntity(join => join.HasData(
                    // "2 + 3"
                    new {QuizItemsId = 1, IncorrectAnswersId = 1},
                    new {QuizItemsId = 1, IncorrectAnswersId = 2},
                    new {QuizItemsId = 1, IncorrectAnswersId = 3},
                    // "2 * 3"
                    new {QuizItemsId = 2, IncorrectAnswersId = 3},
                    new {QuizItemsId = 2, IncorrectAnswersId = 4},
                    new {QuizItemsId = 2, IncorrectAnswersId = 7},
                    // "8 - 3"
                    new {QuizItemsId = 3, IncorrectAnswersId = 1},
                    new {QuizItemsId = 3, IncorrectAnswersId = 3},
                    new {QuizItemsId = 3, IncorrectAnswersId = 9},
                    // "8 : 2"
                    new {QuizItemsId = 4, IncorrectAnswersId = 2},
                    new {QuizItemsId = 4, IncorrectAnswersId = 6},
                    new {QuizItemsId = 4, IncorrectAnswersId = 8},
                    // "7 * 3"
                    new {QuizItemsId = 5, IncorrectAnswersId = 10},
                    new {QuizItemsId = 5, IncorrectAnswersId = 11},
                    new {QuizItemsId = 5, IncorrectAnswersId = 12},
                    // "2 : 2"
                    new {QuizItemsId = 6, IncorrectAnswersId = 11},
                    new {QuizItemsId = 6, IncorrectAnswersId = 12},
                    new {QuizItemsId = 6, IncorrectAnswersId = 13},
                    // "25 * 5"
                    new {QuizItemsId = 7, IncorrectAnswersId = 12},
                    new {QuizItemsId = 7, IncorrectAnswersId = 13},
                    new {QuizItemsId = 7, IncorrectAnswersId = 14},
                    // "43 * 2"
                    new {QuizItemsId = 8, IncorrectAnswersId = 13},
                    new {QuizItemsId = 8, IncorrectAnswersId = 14},
                    new {QuizItemsId = 8, IncorrectAnswersId = 15}
                )
            );
    }
}
