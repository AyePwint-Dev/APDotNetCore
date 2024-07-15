using Microsoft.AspNetCore.Mvc;

namespace APDotNetTrainingBatch4.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult LineChart()
        {
            return View();
        }
        public IActionResult SpLineChart() {
            return View();
        }

    }
}
