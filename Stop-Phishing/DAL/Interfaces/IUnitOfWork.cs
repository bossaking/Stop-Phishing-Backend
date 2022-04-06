using Stop_Phishing.Models;

namespace Stop_Phishing.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UserRepository { get; }
        void Save();
    }
}