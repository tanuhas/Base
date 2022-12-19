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
    public class ProvidersController : Controller
    {
        private readonly WarehouseDbContext warehouseDbContext;
        private char searchString;

        public ProvidersController(WarehouseDbContext warehouseDbContext)
        {
            this.warehouseDbContext = warehouseDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var providers = await warehouseDbContext.Providers.ToListAsync();
            return View(providers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProviderViewModel addProviderRequest)
        {
            var provider = new Provider()
            {
                Id = Guid.NewGuid(),
                Name = addProviderRequest.Name,
                PhoneNumber = addProviderRequest.PhoneNumber,
                Address = addProviderRequest.Address
            };
            await warehouseDbContext.Providers.AddAsync(provider);
            await warehouseDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var provider = await warehouseDbContext.Providers.FirstOrDefaultAsync(x => x.Id == id);

            if (provider != null)
            {

                var viewModel = new UpdateProviderViewModel()
                {
                    Id = provider.Id,
                    Name = provider.Name,
                    PhoneNumber = provider.PhoneNumber,
                    Address = provider.Address
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateProviderViewModel model)
        {
            var provider = await warehouseDbContext.Providers.FindAsync(model.Id);

            if (provider != null)
            {
                provider.Name = model.Name;
                provider.PhoneNumber = model.PhoneNumber;
                provider.Address = model.Address;

                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProviderViewModel model)
        {
            var provider = await warehouseDbContext.Providers.FindAsync(model.Id);

            if (provider != null)
            {
                warehouseDbContext.Providers.Remove(provider);
                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index(string searchInt)
        {
            var providers = from m in warehouseDbContext.Warehouses
                         select m;

            if (!String.IsNullOrEmpty(searchInt))
            {
                providers = providers.Where(s => s.Name.Contains(searchString));
            }

            return View(await providers.ToListAsync());
        }
        public IActionResult Details(Guid Id)
        {
            Provider Provider = warehouseDbContext.Providers
             .Where(c => c.Id == Id).FirstOrDefault();

            return View(Provider);
        }

    }
}
