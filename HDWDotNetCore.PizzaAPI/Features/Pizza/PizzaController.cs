using HDWDotNetCore.PizzaAPI.Db;
using HDWDotNetCore.PizzaAPI.Queries;
using HDWDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HDWDotNetCore.PizzaAPI.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly DapperService _dapperService;

        public PizzaController()
        {
            _dbContext = new AppDbContext();
            _dapperService = new DapperService(ConnectionStrings.stringBuilder.ConnectionString);

        }
        [HttpGet]
        public async Task<IActionResult> GetPizzaTypeAsync()
        {
            var lst = await _dbContext.Pizza.ToListAsync();
            return Ok(lst);
        }
        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtrasAsync()
        {
            var lst = await _dbContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }
        //[HttpGet("Order/{InvoiceNo}")]
        //public async Task<IActionResult> GetOrderAsync(string InvoiceNo)
        //{
        //    var pizzaItem = await _dbContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == InvoiceNo);
        //    var lst = await _dbContext.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == InvoiceNo).ToListAsync();
        //    return Ok
        //        (new
        //        {
        //            order = pizzaItem,
        //            detail = lst
        //        }
        //        );
        //}
        [HttpGet("Order/{InvoiceNo}")]
        public async Task<IActionResult> GetOrderAsync(string InvoiceNo)
        {
            var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>
                        (
                            PizzaQuery.PizzaOrderQuery, new {PizzaOrderInvoiceNo =InvoiceNo}
                        );
            var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
                        (
                            PizzaQuery.PizzaOrderDetailQuery, new { PizzaOrderInvoiceNo = InvoiceNo }
                        );
            var model = new PizzaOrderInvoiceResponse
            {
                Order = item,
                OrderDetail = lst
            };
            return Ok(model);
        }
        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var item = await _dbContext.Pizza.FirstOrDefaultAsync(x=>x.Id == orderRequest.PizzaId);
            var itemPrice = item.Price;
            var total = itemPrice;
            
            if(orderRequest.Extra.Length > 0)
            {
                var lstExtra =await _dbContext.PizzaExtras.Where(x=>orderRequest.Extra.Contains(x.Id)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            PizzaOrderModel orderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };
            List<PizzaOrderDetailModel> pizzaExtras = orderRequest.Extra.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceNo = invoiceNo,
            }).ToList();
        
            await _dbContext.PizzaOrders.AddAsync(orderModel);
            await _dbContext.PizzaOrderDetails.AddRangeAsync(pizzaExtras);
            await _dbContext.SaveChangesAsync();

            OrderResponse orderResponse = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for your order! Enjoy your pizza!",
                TotalAmount = total,
            };
            return Ok(orderResponse);
        }
    }
}
