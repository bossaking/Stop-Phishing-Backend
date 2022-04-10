using System;
using Stop_Phishing.DAL.Interfaces;
using Stop_Phishing.Models;

namespace Stop_Phishing.DAL
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Course> CourseRepository { get; }
        public IGenericRepository<Lesson> LessonRepository { get; }
        public IGenericRepository<Test> TestRepository { get; }
        public IGenericRepository<Question> QuestionRepository { get; }
        public IGenericRepository<Answer> AnswerRepository { get; }

        public UnitOfWork(IGenericRepository<User> userRepository, IGenericRepository<Course> courseRepository, IGenericRepository<Lesson> lessonRepository,
            IGenericRepository<Test> testRepository, IGenericRepository<Question> questionRepository, IGenericRepository<Answer> answerRepository,
            ApplicationDbContext context)
        {
            this.UserRepository = userRepository;
            this.CourseRepository = courseRepository;
            this.LessonRepository = lessonRepository;
            this.TestRepository = testRepository;
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;
            this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}