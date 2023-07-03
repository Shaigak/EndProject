using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;
using MyEndProjectCode.ViewModels;
using System.Security.Claims;

namespace MyEndProjectCode.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(
            ILayoutService layoutService,
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager)
        {
            _layoutService = layoutService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<int> GetUserBasketProductsCount(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null) return 0;
            var basketProductCount = await _context.BasketProducts.Where(bp => bp.Basket.AppUserId == user.Id).SumAsync(bp => bp.Quantity);
            return basketProductCount;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new LayoutVM()
            {
                Settings = _layoutService.GetSettingsData(),
                Count = await GetUserBasketProductsCount(_httpContextAccessor.HttpContext.User),
            };

            return await Task.FromResult(View(model));
        }
    }
}

