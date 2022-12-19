using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Contracts;
using System.Net;
using WebApplication5.Areas.Identity.Data;
using WebApplication5.Models;
using WebApplication5.Models.Domain;
using Contract = WebApplication5.Models.Domain.Contract;

namespace WebApplication5.Controllers
{
    
    public class ContractsController : Controller
    {
        private readonly WarehouseDbContext warehouseDbContext;
        private char searchString;

        public ContractsController(WarehouseDbContext warehouseDbContext)
        {
            this.warehouseDbContext = warehouseDbContext;
        }
        [Authorize(Roles = "Владелец базы,Заведующий,Упаковщик")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contracts = await warehouseDbContext.Contracts.ToListAsync();
            return View(contracts);
        }
        [Authorize(Roles = "Владелец базы,Заведующий")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Владелец базы,Заведующий")]
        [HttpPost]
        public async Task<IActionResult> Add(AddContractViewModel addContractRequest)
        {
            var contract = new Contract()
            {
                Id = Guid.NewGuid(),
                EmployeeFIO = addContractRequest.EmployeeFIO,
                Post = addContractRequest.Post,
                Name = addContractRequest.Name,
                Quantity = addContractRequest.Quantity,
                Price = addContractRequest.Price,
                Name1 = addContractRequest.Name1,
                Quantity1 = addContractRequest.Quantity1,
                Price1 = addContractRequest.Price1,
                Name2 = addContractRequest.Name2,
                Quantity2 = addContractRequest.Quantity2,
                Price2 = addContractRequest.Price2,
                ClientFIO = addContractRequest.ClientFIO,
                PhoneNumber = addContractRequest.PhoneNumber,
                Address = addContractRequest.Address,
                Date = addContractRequest.Date

            };
            await warehouseDbContext.Contracts.AddAsync(contract);
            await warehouseDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Владелец базы,Заведующий")]
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var contract = await warehouseDbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);

            if (contract != null)
            {

                var viewModel = new UpdateContractViewModel()
                {
                    Id = contract.Id,
                    EmployeeFIO = contract.EmployeeFIO,
                    Post = contract.Post,
                    Name = contract.Name,
                    Quantity = contract.Quantity,
                    Price = contract.Price,
                    Name1 = contract.Name1,
                    Quantity1 = contract.Quantity1,
                    Price1 = contract.Price1,
                    Name2 = contract.Name2,
                    Quantity2 = contract.Quantity2,
                    Price2 = contract.Price2,
                    ClientFIO = contract.ClientFIO,
                    PhoneNumber = contract.PhoneNumber,
                    Address = contract.Address,
                    Date = contract.Date
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Владелец базы,Заведующий")]
        [HttpPost]
        public async Task<IActionResult> View(UpdateContractViewModel model)
        {
            var contract = await warehouseDbContext.Contracts.FindAsync(model.Id);

            if (contract != null)
            {
                //contract.Id = model.Id;
                contract.EmployeeFIO = model.EmployeeFIO;
                contract.Post = model.Post;
                contract.Name = model.Name;
                contract.Quantity = model.Quantity;
                contract.Price = model.Price;
                contract.Name1 = model.Name1;
                contract.Quantity1 = model.Quantity1;
                contract.Price1 = model.Price1;
                contract.Name2 = model.Name2;
                contract.Quantity2 = model.Quantity2;
                contract.Price2 = model.Price2;
                contract.ClientFIO = model.ClientFIO;
                contract.PhoneNumber = model.PhoneNumber;
                contract.Address = model.Address;
                contract.Date = model.Date;

                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Владелец базы,Заведующий")]
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateContractViewModel model)
        {
            var contract = await warehouseDbContext.Contracts.FindAsync(model.Id);

            if (contract != null)
            {
                warehouseDbContext.Contracts.Remove(contract);
                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index(string searchInt)
        {
            var contracts = from m in warehouseDbContext.Warehouses
                         select m;

            if (!String.IsNullOrEmpty(searchInt))
            {
                contracts = contracts.Where(s => s.Name.Contains(searchString));
            }

            return View(await contracts.ToListAsync());
        }
        public IActionResult Details(Guid Id)
        {
            Contract Contract = warehouseDbContext.Contracts
             .Where(c => c.Id == Id).FirstOrDefault();

           return View(Contract);
       }

    }
}
