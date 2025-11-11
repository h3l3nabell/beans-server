using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace beans_server.Controllers
{
    [Route("api/v1/stock")]
    public class StockController : Controller
    {
        private static int _stock = 0;
        private readonly IHubContext<StockHub> _stockHub;

        public StockController(IHubContext<StockHub> stockHub)
        {
            _stockHub = stockHub ?? throw new ArgumentNullException(nameof(stockHub));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStock([FromBody] int quantity)
        {
            if (quantity < 0)
                return BadRequest("Illegal Stock Theft!");
            _stock += quantity;
            Console.WriteLine($"Broadcasting stock update: {_stock}");
            await _stockHub.Clients.All.SendAsync("StockUpdated", _stock);
            return Ok(new StockResponse { Stock = _stock });
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseStock([FromBody] int quantity)
        {
            if (quantity > _stock)
                return BadRequest("Insufficient stock.");

            _stock -= quantity;
            await _stockHub.Clients.All.SendAsync("StockUpdated", _stock);
            return Ok(new StockResponse { Stock = _stock });
        }

        [HttpGet("total")]
        public IActionResult GetTotalStock()
        {
            return Ok(new StockResponse { Stock = _stock });
        }

        // Method for testing purposes only
        internal void ResetStock()
        {
            _stock = 0;
        }
    }
}
