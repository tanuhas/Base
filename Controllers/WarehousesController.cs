using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication5.Areas.Identity.Data;
using WebApplication5.Models;
using WebApplication5.Models.Domain;


namespace WebApplication5.Controllers
{
    [Authorize(Roles = "Владелец базы,Заведующий")]
    public class WarehousesController : Controller
    {
        private readonly WarehouseDbContext warehouseDbContext;

        public WarehousesController(WarehouseDbContext warehouseDbContext)
        {
            this.warehouseDbContext = warehouseDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var warehouses = await warehouseDbContext.Warehouses.ToListAsync();
            return View(warehouses);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWarehouseViewModel addWarehouseRequest)
        {
            var warehouse = new Warehouse()
            {
                Id = Guid.NewGuid(),
                Name = addWarehouseRequest.Name,
                PhoneNumber = addWarehouseRequest.PhoneNumber,
                Address = addWarehouseRequest.Address
            };
            await warehouseDbContext.Warehouses.AddAsync(warehouse);
            await warehouseDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var warehouse = await warehouseDbContext.Warehouses.FirstOrDefaultAsync(x => x.Id == id);

            if (warehouse != null)
            {

                var viewModel = new UpdateWarehouseViewModel()
                {
                    Id = warehouse.Id,
                    Name = warehouse.Name,
                    PhoneNumber = warehouse.PhoneNumber,
                    Address = warehouse.Address
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateWarehouseViewModel model)
        {
            var warehouse = await warehouseDbContext.Warehouses.FindAsync(model.Id);

            if (warehouse != null)
            {
                warehouse.Name = model.Name;
                warehouse.PhoneNumber = model.PhoneNumber;
                warehouse.Address = model.Address;

                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateWarehouseViewModel model)
        {
            var warehouse = await warehouseDbContext.Warehouses.FindAsync(model.Id);

            if (warehouse != null)
            {
                warehouseDbContext.Warehouses.Remove(warehouse);
                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var warehouses = from m in warehouseDbContext.Warehouses
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                warehouses = warehouses.Where(s => s.Name.Contains(searchString));
            }

            return View(await warehouses.ToListAsync());
        }

    }
}
