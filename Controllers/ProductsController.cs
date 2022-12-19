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
    
    public class ProductsController : Controller
    {
        private readonly WarehouseDbContext warehouseDbContext;

        public ProductsController(WarehouseDbContext warehouseDbContext)
        {
            this.warehouseDbContext = warehouseDbContext;
        }
        [Authorize(Roles = "Бухгалтер, Владелец базы, Упаковщик, Кладовщик, Заведующий")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await warehouseDbContext.Products.ToListAsync();
            return View(products);
        }
        [Authorize(Roles = "Владелец базы, Кладовщик, Заведующий")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Владелец базы, Кладовщик, Заведующий")]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel addProductRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = addProductRequest.Name,
                Quantity = addProductRequest.Quantity,
                Price = addProductRequest.Price,
                NameP = addProductRequest.NameP,
                IdW = addProductRequest.IdW
            };
            await warehouseDbContext.Products.AddAsync(product);
            await warehouseDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Бухгалтер, Владелец базы, Кладовщик, Заведующий")]
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var product = await warehouseDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {

                var viewModel = new UpdateProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    NameP = product.NameP,
                    IdW = product.IdW
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Бухгалтер, Владелец базы, Кладовщик, Заведующий")]
        [HttpPost]
        public async Task<IActionResult> View(UpdateProductViewModel model)
        {
            var product = await warehouseDbContext.Products.FindAsync(model.Id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Quantity = model.Quantity;
                product.Price = model.Price;
                product.NameP = model.NameP;
                product.IdW = model.IdW;

                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Владелец базы, Кладовщик, Заведующий")]
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductViewModel model)
        {
            var product = await warehouseDbContext.Products.FindAsync(model.Id);

            if (product != null)
            {
                warehouseDbContext.Products.Remove(product);
                await warehouseDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Бухгалтер, Владелец базы, Упаковщик, Кладовщик, Заведующий")]

        public async Task<IActionResult> Index(string searchString)
        {
            var products = from m in warehouseDbContext.Products
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }

            return View(await products.ToListAsync());
        }
    }
}
