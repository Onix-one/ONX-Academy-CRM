﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class DapperCourseService : IDapperCourseService
    {
        private readonly IRepository<Course> _repository;
        public DapperCourseService(IRepository<Course> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Course> GetAll()
        {
            return _repository.GetAll();
        }
        public Task<IEnumerable<Course>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Course GetCourse(int id)
        {
            return _repository.GetEntity(id);
        }
        public void Create(Course item)
        {
            _repository.Create(item);
        }
        public void Update(Course item)
        {
            _repository.Update(item);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}