using Microsoft.AspNetCore.Mvc;

namespace APDotNetTrainingBatch4.MvcChartApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
