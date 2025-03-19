using IKEA.BLL.Services.EmployeeServices;
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
        public IActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
