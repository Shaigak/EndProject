﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;
using MyEndProjectCode.ViewModels.BasketViewModels;
using Newtonsoft.Json;

namespace MyEndProjectCode.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        public BasketController(UserManager<AppUser> userManager, AppDbContext context, IProductService productService )
        {
            _userManager = userManager;
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var basket = await _context.Baskets
               .Include(m => m.BasketProducts)
               .ThenInclude(m => m.Product)
               .Include(m => m.BasketProducts)
               .ThenInclude(m => m.Product)
               .ThenInclude(m => m.ProductImages)
               .FirstOrDefaultAsync(m => m.AppUserId == user.Id);

            BasketListVM model = new();

            if (basket == null) return View(model);

            foreach (var dbBasketProduct in basket.BasketProducts)
            {
                BasketProductVM basketProduct = new BasketProductVM
                {
                    Id = dbBasketProduct.Id,
                    Name = dbBasketProduct.Product.Name,
                    Description = dbBasketProduct.Product.Description,
                    Image = dbBasketProduct.Product.ProductImages.FirstOrDefault()?.Image,
                    Quantity = dbBasketProduct.Quantity,
                    Price = dbBasketProduct.Product.Price,
                    Total = (dbBasketProduct.Product.Price * dbBasketProduct.Quantity),
                };
                model.BasketProducts.Add(basketProduct);

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(BasketAddVM basketAddVM)
        {
            if (!ModelState.IsValid) return BadRequest(basketAddVM);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var product = await _context.Products.FindAsync(basketAddVM.Id);
            if (product == null) return NotFound();

            var basket = await _context.Baskets.FirstOrDefaultAsync(m => m.AppUserId == user.Id);

            if (basket == null)
            {
                basket = new Basket
                {
                    AppUserId = user.Id
                };

                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
            }

            var basketProduct = await _context.BasketProducts
                .FirstOrDefaultAsync(bp => bp.ProductId == product.Id && bp.BasketId == basket.Id);

            if (basketProduct != null)
            {
                basketProduct.Quantity++;
            }

            else
            {
                basketProduct = new BasketProduct
                {
                    BasketId = basket.Id,
                    ProductId = product.Id,
                    Quantity = 1
                };

                await _context.BasketProducts.AddAsync(basketProduct);
            }

            await _context.SaveChangesAsync();
            return Ok(await _context.BasketProducts.Where(bp => bp.Basket.AppUserId == user.Id).SumAsync(bp => bp.Quantity));
        }





        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var basketProduct = await _context.BasketProducts
                .FirstOrDefaultAsync(bp => bp.Id == id
                && bp.Basket.AppUserId == user.Id);

            if (basketProduct == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketProduct.ProductId);
            if (product == null) return NotFound();


            

            _context.BasketProducts.Remove(basketProduct);
            await _context.SaveChangesAsync();
            return Ok(await _context.BasketProducts.Where(bp => bp.Basket.AppUserId == user.Id).SumAsync(bp => bp.Quantity));
        }


        public async Task<IActionResult> IncrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var basketProducts = await _context.BasketProducts
               .FirstOrDefaultAsync(bp => bp.Id == id);

            var count = basketProducts.Quantity++;

            await _context.SaveChangesAsync();

            return Ok(count);
        }


        public async Task<IActionResult> DecrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var basketProducts = await _context.BasketProducts
               .FirstOrDefaultAsync(bp => bp.Id == id);

            var count = basketProducts.Quantity--;

            await _context.SaveChangesAsync();

            return Ok(count);
        }

        //[HttpPost]
        // public IActionResult DecrementProductCount(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    var baskets = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basket"]);

        //    var count = baskets.FirstOrDefault(b => b.ProductId == id).Quantity--;

        //    Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

        //    return Ok(count);
        // }

        //public async Task<IActionResult> BasketProductCountChange(int basketId, int quantity)
        //{
        //    if (basketId < 1) return NotFound();
        //    BasketProduct item = _context.BasketProducts.FirstOrDefault(x => x.Id == basketId);
        //    if (item is null) return NotFound();

        //    item.Quantity = item.Quantity + quantity;

        //    if (item.Quantity == 0) return RedirectToAction("Index", "Cart");

        //    Product product = await _productService.GetById(item.ProductId);

        //    _context.SaveChanges();
        //    //item.Count= ++quantity;
        //    return RedirectToAction("Index", "Cart");
        //}


    }
}
