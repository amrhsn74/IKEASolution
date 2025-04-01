using IKEA.BLL.DTOs.Departments;
using IKEA.BLL.DTOs.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.DAL.Models.Employees;
using IKEA.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeService;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;
        public EmployeeController(IEmployeeServices employeeService,ILogger<EmployeeController> logger,IWebHostEnvironment environment)
        {
            this.employeeService = employeeService;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Employees = employeeService.GetAllEmployees();
            return View(Employees);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = employeeService.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
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
        public IActionResult Create(EmployeeVM employeeVM)
        {
            // Server-Side Validation
            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {
                var employeeDto = new CreatedEmployeeDto()
                {
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    HiringDate = employeeVM.HiringDate,
                };
                var result = employeeService.CreatedEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Error in Creating Department";
            }
            catch (Exception ex)
            {
                // 1. Log Kestral Exception 
                logger.LogError(ex, ex.Message);

                // 2. Set Default Error Message
                if (environment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "Error in Creating Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
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
            var employee = employeeService.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            var MappedEmployee= new EmployeeVM
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                IsActive = employee.IsActive,
            };
            return View(MappedEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            var message = string.Empty;
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    HiringDate = employeeVM.HiringDate,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Salary = employeeVM.Salary,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    IsActive = employeeVM.IsActive,
                };
                var result = employeeService.UpdateEmployee(employeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Error in Updating Employee";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                logger.LogError(ex, ex.Message);

                // 2. Set Default Error Message
                message = environment.IsDevelopment() ? ex.Message : "Error in Updating Employee";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
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
            var employee = employeeService.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int EmpId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = employeeService.DeleteEmployee(EmpId);
                if (IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Error in Deleting Employee";

            }
            catch (Exception ex)
            {
                // 1. Log Exception
                logger.LogError(ex, ex.Message);
                // 2. Set Default Error Message
                message = environment.IsDevelopment() ? ex.Message : "Error in Deleting Employee";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { id = EmpId });
        }
        #endregion
    }
}
