using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.WebAPI.Dto;

namespace ONX.CRM.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        [HttpGet("GetStudents")]
        public async Task<IEnumerable<StudentsDto>> GetStudents()
        {

            var students = _mapper.Map<IEnumerable<StudentsDto>>(await _studentService.GetAllAsync()).ToList();
            return students;
        }
    }
}
