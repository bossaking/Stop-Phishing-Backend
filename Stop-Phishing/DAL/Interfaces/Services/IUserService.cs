using System.Threading.Tasks;
using Stop_Phishing.Models;

namespace Stop_Phishing.DAL.Interfaces.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByEmail(string email);
    }
}