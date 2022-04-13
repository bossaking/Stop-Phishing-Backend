using Stop_Phishing.Models;

namespace Stop_Phishing.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public ApplicationDbContext Context { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Course> CourseRepository { get; }
        IGenericRepository<Lesson> LessonRepository { get; }
        public IGenericRepository<Test> TestRepository { get; }
        public IGenericRepository<Question> QuestionRepository { get; }
        public IGenericRepository<Answer> AnswerRepository { get; }
        void Save();
    }
}