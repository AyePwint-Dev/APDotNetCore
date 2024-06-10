using APDotNetTrainingBatch4.PizzaApi.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch4.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public PizzaController()
        {
            _appDbContext = new AppDbContext();
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _appDbContext.Pizza.ToListAsync();
            return Ok(lst);
        }
        [HttpGet("Extras")]
        public async Task<IActionResult> GetPizzaExtraAsync()
        {
            var lst = await _appDbContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }
        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var itemPizza = await _appDbContext.Pizza.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = itemPizza.Price;

            if(orderRequest.Extras.Length > 0)
            {
               var lstExtra = await _appDbContext.PizzaExtras.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync(); ;
                total += lstExtra.Sum(x => x.Price);
            }
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };
            List<PizzaOrderDetailModel> pizzaExtraModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceNo = invoiceNo
            }).ToList();

            await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
            await _appDbContext.SaveChangesAsync();

            OrderResponse response = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for your order! Enjoy your pizza!",
                TotalAmount = total,
            };
            return Ok(response);
        }
        [HttpGet("Order/{invoiceNo}")]
        public async Task<IActionResult> GetOrder(string invoiceNo)
        {
            var item = await _appDbContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
            var lst = await _appDbContext.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();
            return Ok(new
            {
                Order = item,
                OrderDetail = lst
            });
        }
    }
}
