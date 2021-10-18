using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class CoursesController : Controller
    {
        private readonly IEntityService<Specialization> _specializationService;
        private readonly IEntityService<Course> _courseService;
        private readonly IMapper _mapper;
        private readonly ILogger<CoursesController> _logger;
        public CoursesController(IEntityService<Course> courseService, 
            IEntityService<Specialization> specializationService, 
            IMapper mapper, ILogger<CoursesController> logger)
        {
            _specializationService = specializationService;
            _courseService = courseService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.SpecializationTitle = _specializationService.GetEntityById(id).Title;
            var courses = await _courseService.GetAllAsync();
            return View(_mapper
                .Map<IEnumerable<CourseViewModel>>(courses.Where(_ => _.SpecializationId == id)).ToList());
        }
    }
}
