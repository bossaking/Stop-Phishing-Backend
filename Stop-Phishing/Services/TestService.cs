using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Stop_Phishing.DAL.Interfaces;
using Stop_Phishing.DAL.Interfaces.Services;
using Stop_Phishing.DTO.Course;
using Stop_Phishing.DTO.Test;
using Stop_Phishing.Helpers.Token;
using Stop_Phishing.Models;

namespace Stop_Phishing.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TestResponse> GetTestByCourseIdAsync(Guid id)
        {
            var test = (await _unitOfWork.TestRepository.GetWhere(t => t.CourseId.Equals(id))).First();
            return new TestResponse()
            {
                Id = test.Id,
                Questions = test.Questions.Select(q => new SimpleQuestion()
                {
                    Id = q.Id,
                    Title = q.Title,
                    RightAnswerCommunicate = q.RightAnswerCommunicate,
                    BadAnswerCommunicate = q.BadAnswerCommunicate,
                    Image = q.Image,
                    Answers = q.Answers.Select(a => new SimpleAnswer()
                    {
                        Id = a.Id,
                        Title = a.Title,
                        IsRight = a.IsRight
                    })
                })
            };
        }

        public async Task<TestResponse> GetTestByIdAsync(Guid id)
        {
            var test = (await _unitOfWork.TestRepository.GetWhere(t => t.Id.Equals(id))).First();
            return new TestResponse()
            {
                Id = test.Id,
                Questions = test.Questions.Select(q => new SimpleQuestion()
                {
                    Id = q.Id,
                    Title = q.Title,
                    RightAnswerCommunicate = q.RightAnswerCommunicate,
                    BadAnswerCommunicate = q.BadAnswerCommunicate,
                    Image = q.Image,
                    Answers = q.Answers.Select(a => new SimpleAnswer()
                    {
                        Id = a.Id,
                        Title = a.Title,
                        IsRight = a.IsRight
                    })
                })
            };
        }

        private async Task<string> SaveImage(IFormFile file)
        {
            var imagePath = "";
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value;
            fileName = Guid.NewGuid() + "-" + fileName;
            var fullPath = Path.Combine(pathToSave, fileName);
            imagePath = Path.Combine("Images", fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return imagePath;
        }

        public async Task<ResultMessage> CreateTestAsync(CreateTestRequest testRequest, Guid courseId)
        {
            var course = (await _unitOfWork.CourseRepository.GetWhere(c => c.Id.Equals(courseId))).First();
            var test = new Test()
            {
                CourseId = courseId,
                Course = course
            };

            var questions = new List<Question>();
            foreach (var question in testRequest.Questions)
            {
                var quest = new Question()
                {
                    Title = question.Title,
                    RightAnswerCommunicate = question.RightAnswerCommunicate,
                    BadAnswerCommunicate = question.BadAnswerCommunicate,
                    Test = test
                };

                if (question.ImageName != null)
                {
                    quest.Image = await SaveImage(testRequest.Images.First(i => i.FileName.Equals(question.ImageName)));
                }

                var answers = new List<Answer>();

                foreach (var answer in question.Answers)
                {
                    var ans = new Answer()
                    {
                        Title = answer.Title,
                        IsRight = answer.IsRight,
                        Question = quest
                    };
                    answers.Add(ans);
                    await _unitOfWork.AnswerRepository.Insert(ans);
                }

                quest.Answers = answers;
                questions.Add(quest);
                await _unitOfWork.QuestionRepository.Insert(quest);
            }

            test.Questions = questions;
            await _unitOfWork.TestRepository.Insert(test);
            _unitOfWork.Save();

            return new ResultMessage() {Status = true, Message = "Test został utworzony pomyślnie"};
        }

        public async Task<ResultMessage> UpdateTestAsync(UpdateTestRequest testRequest, Guid testId)
        {
            var test = await _unitOfWork.TestRepository.GetById(testId);
            if (testRequest.NewQuestions is not null)
            {
                foreach (var newQuestion in testRequest.NewQuestions)
                {
                    var quest = new Question()
                    {
                        Title = newQuestion.Title,
                        RightAnswerCommunicate = newQuestion.RightAnswerCommunicate,
                        BadAnswerCommunicate = newQuestion.BadAnswerCommunicate,
                        Test = test
                    };

                    if (newQuestion.ImageName != null)
                    {
                        quest.Image =
                            await SaveImage(testRequest.Images.First(i => i.FileName.Equals(newQuestion.ImageName)));
                    }

                    var answers = new List<Answer>();

                    foreach (var answer in newQuestion.Answers)
                    {
                        var ans = new Answer()
                        {
                            Title = answer.Title,
                            IsRight = answer.IsRight,
                            Question = quest
                        };
                        answers.Add(ans);
                        await _unitOfWork.AnswerRepository.Insert(ans);
                    }

                    quest.Answers = answers;
                    await _unitOfWork.QuestionRepository.Insert(quest);
                }
            }

            if (testRequest.DeletedQuestions is not null)
            {
                foreach (var deletedTestId in testRequest.DeletedQuestions)
                {
                    await _unitOfWork.QuestionRepository.Delete(deletedTestId);
                }
            }

            if (testRequest.EditedQuestions is not null)
            {
                foreach (var question in testRequest.EditedQuestions)
                {
                    if (question.DeletedAnswers is not null)
                    {
                        foreach (var deletedAnswerId in question.DeletedAnswers)
                        {
                            await _unitOfWork.AnswerRepository.Delete(deletedAnswerId);
                        }
                    }

                    if (question.AddedAnswers is not null)
                    {
                        foreach (var newAnswer in question.AddedAnswers)
                        {
                            var answer = new Answer()
                            {
                                Title = newAnswer.Title,
                                IsRight = newAnswer.IsRight,
                                QuestionId = question.Id
                            };

                            await _unitOfWork.AnswerRepository.Insert(answer);
                        }
                    }

                    if (question.EditedAnswers is not null)
                    {
                        foreach (var editedAnswer in question.EditedAnswers)
                        {
                            var answer = new Answer()
                            {
                                Id = editedAnswer.Id,
                                Title = editedAnswer.Title,
                                IsRight = editedAnswer.IsRight,
                                QuestionId = question.Id
                            };
                            _unitOfWork.AnswerRepository.Update(answer);
                        }
                    }

                    var quest = new Question()
                    {
                        Id = question.Id,
                        Title = question.Title,
                        RightAnswerCommunicate = question.RightAnswerCommunicate,
                        BadAnswerCommunicate = question.BadAnswerCommunicate,
                        Test = test
                    };

                    if (question.ImageName != null)
                    {
                        quest.Image =
                            await SaveImage(testRequest.Images.First(i => i.FileName.Equals(question.ImageName)));
                    }

                    _unitOfWork.QuestionRepository.Update(quest);

                    if (question.ImageName == null)
                    {
                        _unitOfWork.Context.Entry(quest).Property(q => q.Image).IsModified = false;
                    }
                }
            }

            _unitOfWork.Save();

            return new ResultMessage() {Status = true, Message = "Test został zaktualizowany pomyślnie"};
        }

        public async Task<ResultMessage> RemoveTestAsync(Guid id)
        {
            await _unitOfWork.TestRepository.Delete(id);
            _unitOfWork.Save();
            return new ResultMessage() {Status = true, Message = "Test został usunięty pomyślnie"};
        }
    }
}