using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<AllCoursesResponse> GetAllCoursesAsync()
        {
            var courses = await _unitOfWork.CourseRepository.GetWhere();
            return new AllCoursesResponse()
            {
                Courses = courses.Select(c => new SimpleCourse()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Title = c.Title,
                    Description = c.Description,
                    Lessons = c.Lessons.Select(l => new SimpleLesson()
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Description = l.Description
                    })
                })
            };
        }

        public async Task<Course> GetCourseByIdAsync(Guid id) =>
            (await _unitOfWork.CourseRepository.GetById(id));

        public async Task<ResultMessage> CreateCourseAsync(CreateCourseRequest courseRequest)
        {
            var course = new Course()
            {
                Name = courseRequest.Name,
                Title = courseRequest.Title,
                Description = courseRequest.Description,
                IsComplete = false
            };

            foreach (var lesson in courseRequest.Lessons)
            {
                var les = new Lesson()
                {
                    Title = lesson.Title,
                    Description = lesson.Description,
                    Course = course
                };
                await _unitOfWork.LessonRepository.Insert(les);
            }
            
            await _unitOfWork.CourseRepository.Insert(course);
            _unitOfWork.Save();

            return new ResultMessage() {Status = true, Message = "Lekcja została utworzona pomyślnie"};
        }

        public ResultMessage UpdateCourseAsync(CreateCourseRequest courseRequest, Guid id)
        {
            var course = new Course()
            {
                Id = id,
                Title = courseRequest.Title,
                Name = courseRequest.Name,
                Description = courseRequest.Description,
                Lessons = courseRequest.Lessons.Select(l => new Lesson()
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    CourseId = id
                })
            };

            _unitOfWork.CourseRepository.Update(course);
            _unitOfWork.Save();
            return new ResultMessage() {Status = true, Message = "Zmiany zostały zapisane"};
        }

        public async Task<ResultMessage> RemoveCourseAsync(Guid id)
        {
            await _unitOfWork.CourseRepository.Delete(id);
            _unitOfWork.Save();
            return new ResultMessage() {Status = true, Message = "Lekcja została usunięta pomyślnie"};
        }
    }
}