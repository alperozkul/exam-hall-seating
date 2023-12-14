using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.ClassroomVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Controllers
{
    public class ClassroomController : Controller
    {    
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomDetailRepository _classroomDetailRepository;
        private readonly IMapper _mapper;

        private static CreateClassroomViewModel classroomViewModel = new CreateClassroomViewModel();

        public ClassroomController(IClassroomRepository classroomRepository, IMapper mapper, IClassroomDetailRepository classroomDetailRepository)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper;
            _classroomDetailRepository = classroomDetailRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Classroom> images =  _classroomRepository.GetAll();
            classroomViewModel = new CreateClassroomViewModel();
            return View(images);
        }

        public IActionResult Create()
        {
            return View(classroomViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClassroomViewModel model, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        image.CopyTo(memoryStream);
                        model.ClassroomData.ImageData = memoryStream.ToArray();
                    }
                }
                _classroomRepository.Add(model.ClassroomData);
                Classroom classroom = _classroomRepository.GetByName(model.ClassroomData.ClassName);

                foreach(var block in classroomViewModel.ClassroomDetail)
                {
                    block.ClassroomId = classroom.Id;
                }
                int totalCapacity = await _classroomDetailRepository.CreateClassroomBlocksAsync(classroomViewModel.ClassroomDetail);
                classroom.ExamCapacity = totalCapacity;
                _classroomRepository.Update(classroom);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int id)
        {
            DetailClassroomViewModel detailClassroomVM = new DetailClassroomViewModel();

            Classroom classroom = await _classroomRepository.GetByIdAsync(id);
            detailClassroomVM.Classroom = classroom;

            List<ClassroomDetail> classroomDetails = _classroomDetailRepository.GetAllDetailById(id);     
            foreach(var detail in classroomDetails)
            {
                detailClassroomVM.ClassroomDetail.Add(detail);
            }
            return View(detailClassroomVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Classroom classroom = await _classroomRepository.GetByIdAsync(id);
            _classroomRepository.Delete(classroom);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddBlock(CreateClassroomViewModel model)
        {
            var newClassroomDetail = new ClassroomDetail
            {
				BlockNo = model.BlockNumber,
                Row = model.Row,
                Column = model.Column,
                ValidColumns = model.ValidColumns,                
            };

            //Aynı blok numarasına sahip başka kayıt var mı?
            if (classroomViewModel.ClassroomDetail.Any(b => b.BlockNo == newClassroomDetail.BlockNo))
            {
                ModelState.AddModelError(string.Empty, "Blok numarası zaten kullanılıyor.");
                return View("Create", classroomViewModel);
            }
            //Blok bilgileri eklenir.
            classroomViewModel.ClassroomDetail.Add(newClassroomDetail);

            //ClassroomDetail listesi Blok numarasına göre sıralanır.
            classroomViewModel.ClassroomDetail = classroomViewModel.ClassroomDetail.OrderBy(detail => detail.BlockNo).ToList();
            

            return RedirectToAction("Create", classroomViewModel);
        }

        [HttpPost]
        public IActionResult DeleteBlock(int blockNo)
        {
            var blok = classroomViewModel.ClassroomDetail.FirstOrDefault(b => b.BlockNo == blockNo);
            if (blok != null)
            {
                classroomViewModel.ClassroomDetail.Remove(blok);
            }
            return RedirectToAction("Create", classroomViewModel);
        }

        

    }
}
