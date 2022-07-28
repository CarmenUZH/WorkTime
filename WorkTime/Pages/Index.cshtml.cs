using Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace WorkTime.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly csvreader thereader;
        private readonly ISQLQuery sqlquery;
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatacollector _datacollector;
        public IEnumerable<Day> Workdays;

        public IndexModel(ILogger<IndexModel> logger, IDatacollector datacollector, ISQLQuery sQLQuery)
        {
            //thereader = new csvreader();
            sqlquery = sQLQuery;
            _logger = logger;
            _datacollector = datacollector;
            Workdays = _datacollector.getData();


        }

        public void OnGet()
        {
            _datacollector.Commit();
           // sqlquery.CreatePostGres(); 
            //thereader.readcsv();
        }
    }
}