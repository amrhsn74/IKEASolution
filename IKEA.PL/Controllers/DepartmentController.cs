using AutoMapper;
using Azure.Identity;
using IKEA.BLL.DTOs.Departments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IKEA.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Services - DI
        private readonly IDepartmentServices departmentServices;
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices,IMapper mapper,ILogger<DepartmentController> _logger,IWebHostEnvironment _environment)
        {
            departmentServices = _departmentServices;
            this.mapper = mapper;
            logger = _logger;
            environment = _environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Departments = await departmentServices.GetAllDepartments();
            return View(Departments);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = await departmentServices.GetDepartmentById(id.Value);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentVM departmentVM)
        {
            // Server-Side Validation
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                var departmentDto = mapper.Map<DepartmentVM,CreatedDepartmentDto>(departmentVM);
                
                //var departmentDto = new CreatedDepartmentDto
                //{
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    Description = departmentVM.Description
                //};
                var result = await departmentServices.CreatedDepartment(departmentDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Error in Creating Department";
            }
            catch (Exception ex)
            {
                // 1. Log Kestral Exception 
                logger.LogError(ex,ex.Message);

                // 2. Set Default Error Message
                if(environment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "Error in Creating Department";
 
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = await departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            var MappedDepartment = mapper.Map<DepartmentDetailsDto, DepartmentVM>(department);

            //var MappedDepartment = new DepartmentVM
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate
            //};
            return View(MappedDepartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                var departmentDto = mapper.Map<DepartmentVM, UpdatedDepartmentDto>(departmentVM);

                //var departmentDto = new UpdatedDepartmentDto
                //{
                //    Id = departmentVM.Id,
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    Description = departmentVM.Description
                //};
                var result = await departmentServices.UpdateDepartment(departmentDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Error in Updating Department";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                logger.LogError(ex, ex.Message);

                // 2. Set Default Error Message
                message = environment.IsDevelopment() ? ex.Message : "Error in Updating Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = await departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int DeptId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = await departmentServices.DeleteDepartment(DeptId);
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
