using Microsoft.AspNetCore.Mvc;
using MyEndProjectCode.Services.Interfaces;
using MyEndProjectCode.ViewModels;

namespace MyEndProjectCode.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new LayoutVM()
            {
                Settings = _layoutService.GetSettingsData()
            };
            return await Task.FromResult(View(model));
        }
    }
}

