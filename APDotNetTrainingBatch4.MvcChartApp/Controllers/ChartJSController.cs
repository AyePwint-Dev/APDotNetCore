using Microsoft.AspNetCore.Mvc;

namespace APDotNetTrainingBatch4.MvcChartApp.Controllers
{
    public class ChartJSController : Controller
    {
        private readonly ILogger<CanvasJsController> _logger;

        public ChartJSController(ILogger<CanvasJsController> logger)
        {
            _logger = logger;
        }

        public IActionResult ExampleChart()
        {
            return View();
        }
        public IActionResult InterpolationLineChart()
        {
            return View();
        }
        public IActionResult ProgressiveChart() {
            _logger.LogInformation("ProgressiveChart Controller Start....");
            return View();
        }
    }
}
