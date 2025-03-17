using Azure.Identity;
using IKEA.BLL.DTOs.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices,ILogger<DepartmentController> _logger,IWebHostEnvironment _environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            environment = _environment;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

        #region Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            // Server-Side Validation
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var message = string.Empty;
            try
            {
                var result = departmentServices.CreatedDepartment(departmentDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Error in Creating Department";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                // 1. Log Kestral Exception 
                logger.LogError(ex,ex.Message);

                // 2. Set Default Error Message
                if(environment.IsDevelopment())
                {
                    message = ex.Message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDto);
                }
                else
                {
                    message = "Error in Creating Department";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDto);
                }
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            var MappedDepartment = new UpdatedDepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate
            };
            return View(MappedDepartment);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var message = string.Empty;
            try
            {
                var result = departmentServices.UpdateDepartment(departmentDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Error in Updating Department";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                logger.LogError(ex, ex.Message);

                // 2. Set Default Error Message
                message = environment.IsDevelopment() ? ex.Message : "Error in Updating Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentDto);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        public IActionResult Delete(int DeptId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(DeptId);
                if (IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Error in Deleting Department";
                 
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                logger.LogError(ex, ex.Message);
                // 2. Set Default Error Message
                message = environment.IsDevelopment() ? ex.Message : "Error in Deleting Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new {id = DeptId});
        }
        #endregion
    }
}
