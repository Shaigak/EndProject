using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.ViewModels.BasketViewModels;
using MyEndProjectCode.ViewModels.WishViewModels;

namespace MyEndProjectCode.Controllers
{
    public class WishController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public WishController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var wish = await _context.Wishs
               .Include(m => m.WishProducts)
               .ThenInclude(m => m.Product)
               .Include(m => m.WishProducts)
               .ThenInclude(m => m.Product)
               .ThenInclude(m => m.ProductImages)
               .FirstOrDefaultAsync(m => m.AppUserId == user.Id);

            WishListVM model = new();

            if (wish == null) return View(model);

            foreach (var dbWishProduct in wish.WishProducts)
            {
                WishProductVM wishProduct = new WishProductVM
                {
                    Id = dbWishProduct.Id,
                    Name = dbWishProduct.Product.Name,
                    Description = dbWishProduct.Product.Description,
                    Image = dbWishProduct.Product.ProductImages.FirstOrDefault()?.Image,
                    Quantity = dbWishProduct.Quantity,
                    Price = dbWishProduct.Product.Price,
                    Total = (dbWishProduct.Product.Price * dbWishProduct.Quantity),
                };
                model.WishProducts.Add(wishProduct);

            }

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(WishAddVM wishAddVM)
        {
            if (!ModelState.IsValid) return BadRequest(wishAddVM);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var product = await _context.Products.FindAsync(wishAddVM.Id);
            if (product == null) return NotFound();

            var wish = await _context.Wishs.FirstOrDefaultAsync(m => m.AppUserId == user.Id);

            if (wish == null)
            {
                wish = new Wish
                {
                    AppUserId = user.Id
                };

                await _context.Wishs.AddAsync(wish);
                await _context.SaveChangesAsync();
            }

            var wishProduct = await _context.WishProducts
                .FirstOrDefaultAsync(bp => bp.ProductId == product.Id && bp.WishId == wish.Id);

            if (wishProduct != null)
            {
                wishProduct.Quantity++;
            }

            else
            {
                wishProduct = new WishProduct
                {
                    WishId = wish.Id,
                    ProductId = product.Id,
                    Quantity = 1
                };

                await _context.WishProducts.AddAsync(wishProduct);
            }

            await _context.SaveChangesAsync();
            return Ok(await _context.WishProducts.Where(bp => bp.Wish.AppUserId == user.Id).SumAsync(bp => bp.Quantity));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var wishProduct = await _context.WishProducts
                .FirstOrDefaultAsync(bp => bp.Id == id
                && bp.Wish.AppUserId == user.Id);

            if (wishProduct == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == wishProduct.ProductId);
            if (product == null) return NotFound();




            _context.WishProducts.Remove(wishProduct);
            await _context.SaveChangesAsync();
            return Ok(await _context.WishProducts.Where(bp => bp.Wish.AppUserId == user.Id).SumAsync(bp => bp.Quantity));
        }

    }
}
