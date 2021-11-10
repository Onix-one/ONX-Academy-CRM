using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;
using ONX.CRM.Filters;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Controllers
{
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class LessonsController : BaseController
    {
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public LessonsController(ILessonService lessonService, IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _lessonService = lessonService;
            _userService = userService;

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id, int groupId)
        {
            return View(id.HasValue
                ? _mapper.Map<LessonViewModel>(await _lessonService.GetEntityByIdAsync(id.Value))
                : new LessonViewModel { GroupId = groupId });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LessonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Id != 0)
            {
                _lessonService.Update(_mapper.Map<Lesson>(model));
            }
            else
            {
                _lessonService.Create(_mapper.Map<Lesson>(model));
            }
            return RedirectToAction("GroupInfo", "Teachers", new { id = model.GroupId });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var groupId = _lessonService.GetEntityByIdAsync(id).Result.GroupId;
            _lessonService.Delete(id);
            return RedirectToAction("GroupInfo", "Teachers", new { id = groupId });
        }

        [HttpPost]
        public async Task<IActionResult> UploadMaterial(int id, IFormFile uploadedFile)
        {
            var groupId = _lessonService.GetEntityByIdAsync(id).Result.GroupId;
            if (uploadedFile != null)
            {
                if (uploadedFile.Length > 600000)
                {
                    return RedirectToAction("GroupInfo", "Teachers", new { id = groupId, error = "error" });
                }
                //save file to DB (Person)
                await using var memoryStream = new MemoryStream();

                await uploadedFile.CopyToAsync(memoryStream);

                var content = memoryStream.ToArray();

                _lessonService.SaveMaterial(id, content);
            }

            return RedirectToAction("GroupInfo", "Teachers", new { id = groupId });
        }
        public IActionResult DeleteMaterial(int id)
        {
            var groupId = _lessonService.GetEntityByIdAsync(id).Result.GroupId;
            _lessonService.DeleteMaterial(id);
            return RedirectToAction("GroupInfo", "Teachers", new { id = groupId });
        }
        public async Task<IActionResult> DownloadMaterial(int id)
        {
            var lesson = await _lessonService.GetEntityByIdAsync(id);
            byte[] material = lesson.Materials;
            if (material != null)
            {
                const string fileType = "application/pdf";
                var fileName = $"material {lesson.Number}.pdf";
                return File(material, fileType, fileName);
            }
            return null;
        }
    }
}
