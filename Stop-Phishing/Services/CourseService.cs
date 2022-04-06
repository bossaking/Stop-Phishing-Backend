using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stop_Phishing.DAL.Interfaces;
using Stop_Phishing.DAL.Interfaces.Services;
using Stop_Phishing.DTO.Course;
using Stop_Phishing.Helpers.Token;
using Stop_Phishing.Models;

namespace Stop_Phishing.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync() => 
        ( await _unitOfWork.CourseRepository.GetWhere());

        public async Task<Course> GetCourseByIdAsync(Guid id) =>
            (await _unitOfWork.CourseRepository.GetById(id));

        public async Task<ResultMessage> CreateCourseAsync(CreateCourseRequest courseRequest)
        {
            var course = new Course()
            {
                Name = courseRequest.Name,
                Title = courseRequest.Title,
                Description = courseRequest.Description
            };
            await _unitOfWork.CourseRepository.Insert(course);
            _unitOfWork.Save();

            return new ResultMessage() {Status = true, Message = "Lekcja została utworzona pomyślnie"};
        }
    }
}