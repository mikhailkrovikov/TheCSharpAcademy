using Flashcards.DTOs;
using Flashcards.Entities;
using Flashcards.Repositories;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Controllers
{
    public class StudySessionController
    {
        private readonly StudySessionRepository repository;

        public StudySessionController()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            repository = new(connectionString);
            repository.CreateTable();
        }

        public void CreateSession(CreateStudySessionDTO studySession)
        {
            var entity = new StudySessionEntity
            {
                CardStackId = studySession.CardStackId,
                Time = studySession.Time,
                Score = studySession.Score
            };
            repository.CreateSession(entity);
        }
    }
}
