using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace WorkTime.Pages
{
    public class AddModel : PageModel
    {
        private readonly IHtmlHelper htmlHelper;
        private readonly IDatacollector datacollector;
        [BindProperty] //Make Day be Post-usable
        public Day newday { get; set; }

        public AddModel(IDatacollector datacollector, IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.datacollector = datacollector;
        }


        public IActionResult OnGet()
        {
            newday = new Day();
            return Page();
        }
        public IActionResult OnPost()
        {
            datacollector.Add(newday);
            datacollector.Commit();
            return RedirectToPage("./Index");

        }
    }
}
