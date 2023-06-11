using MyEndProjectCode.Data;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
       
        
        public Dictionary<string, string> GetSettingsData()
        {
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            return settings;
        }

    }
}
