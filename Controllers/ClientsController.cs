using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication5.Areas.Identity.Data;
using WebApplication5.Models;
using WebApplication5.Models.Domain;


namespace WebApplication5.Controllers
{
    [Authorize(Roles = "Владелец базы,Заведующий,Упаковщик")]
    public class ClientsController : Controller
    {
        private readonly WarehouseDbContext warehouseDbContext;
        private char searchString;

        public ClientsController(WarehouseDbContext warehouseDbContext)
        {
            this.warehouseDbContext = warehouseDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clients = await warehouseDbContext.Clients.ToListAsync();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClientViewModel addClientRequest)
        {
            var client = new Client()
            {
                Id = Guid.NewGuid(),
                Name = addClientRequest.Name,
                PhoneNumber = addClientRequest.PhoneNumber,
                Address = addClientRequest.Address
            };
            await warehouseDbContext.Clients.AddAsync(client);
            await warehouseDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var client = await warehouseDbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (client != null)
            {

                var viewModel = new UpdateClientViewModel()
                {
                    Id = client.Id,
                    Name = client.Name,
                    PhoneNumber = client.PhoneNumber,
                    Address = client.Address
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateClientViewModel model)
        {
            var client = await warehouseDbContext.Clients.FindAsync(model.Id);

            if (client != null)
            {
                client.Name = model.Name;
                client.PhoneNumber = model.PhoneNumber;
                client.Address = model.Address;

                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateClientViewModel model)
        {
            var client = await warehouseDbContext.Clients.FindAsync(model.Id);

            if (client != null)
            {
                warehouseDbContext.Clients.Remove(client);
                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index(string searchInt)
        {
            var clients = from m in warehouseDbContext.Warehouses
                         select m;

            if (!String.IsNullOrEmpty(searchInt))
            {
                clients = clients.Where(s => s.Name.Contains(searchString));
            }

            return View(await clients.ToListAsync());
        }

    }
}
