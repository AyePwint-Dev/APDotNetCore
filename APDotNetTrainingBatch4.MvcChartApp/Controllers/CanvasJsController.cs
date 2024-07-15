using Microsoft.AspNetCore.Mvc;

namespace APDotNetTrainingBatch4.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        private readonly ILogger<CanvasJsController> _logger;

		public CanvasJsController(ILogger<CanvasJsController> logger)
		{
			_logger = logger;
		}

		public IActionResult LineChart()
        {
            _logger.LogInformation("LineChart Controller....");
            return View();
        }
        public IActionResult SpLineChart() {
            return View();
        }

    }
}
