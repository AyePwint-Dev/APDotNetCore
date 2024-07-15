using Microsoft.AspNetCore.Mvc;

namespace APDotNetTrainingBatch4.MvcChartApp.Controllers
{
    public class ChartJSController : Controller
    {
        public IActionResult ExampleChart()
        {
            return View();
        }
        public IActionResult InterpolationLineChart()
        {
            return View();
        }
    }
}
