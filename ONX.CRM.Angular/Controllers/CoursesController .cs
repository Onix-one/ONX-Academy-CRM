using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONX.CRM.Angular.Dto;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.Angular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {

        private readonly IEntityService<Course> _courseService;
        private readonly IMapper _mapper;
        public CoursesController(IEntityService<Course> courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseDto>> Get()
        {
            return _mapper.Map<IEnumerable<CourseDto>>(await _courseService.GetAllAsync());
        }
    }
}
