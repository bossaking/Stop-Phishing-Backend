using System.Linq;
using System.Threading.Tasks;
using Stop_Phishing.DAL.Interfaces;
using Stop_Phishing.DAL.Interfaces.Services;
using Stop_Phishing.Models;

namespace Stop_Phishing.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> GetUserByEmail(string email) => 
            (await _unitOfWork.UserRepository.GetWhere(x => x.Email == email)).FirstOrDefault();      
    }
}