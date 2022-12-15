using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication5.Areas.Identity.Data;
using WebApplication5.Models;
using WebApplication5.Models.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly WarehouseDbContext warehouseDbContext;

        public EmployeesController(WarehouseDbContext warehouseDbContext)
        {
            this.warehouseDbContext = warehouseDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await warehouseDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FIO = addEmployeeRequest.FIO,
                Post = addEmployeeRequest.Post,
                Salary = addEmployeeRequest.Salary
            };
            await warehouseDbContext.Employees.AddAsync(employee);
            await warehouseDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await warehouseDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {

                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    FIO = employee.FIO,
                    Post = employee.Post,
                    Salary =  employee.Salary
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await warehouseDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                //employee.Id = model.Id;
                employee.FIO = model.FIO;
                employee.Post = model.Post;
                employee.Salary = model.Salary;

                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await warehouseDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                warehouseDbContext.Employees.Remove(employee);
                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = from m in warehouseDbContext.Employees
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.Post.Contains(searchString));
            }

            return View(await employees.ToListAsync());
        }
    }
}
