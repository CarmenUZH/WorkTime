using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace WorkTime.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly csvreader thereader;
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatacollector _datacollector;
        public IEnumerable<Day> Workdays;

        public IndexModel(ILogger<IndexModel> logger, IDatacollector datacollector)
        {
            //thereader = new csvreader();
            _logger = logger;
            _datacollector = datacollector;

        }

        public void OnGet()
        {
            Workdays = _datacollector.getData();
            _datacollector.Commit();
            //thereader.readcsv();
        }
    }
}