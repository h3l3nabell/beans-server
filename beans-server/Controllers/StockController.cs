using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace beans_server.Controllers
{
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
            _stock += quantity;
            await _stockHub.Clients.All.SendAsync("StockUpdated", _stock);
            return Ok(new { Stock = _stock });
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseStock([FromBody] int quantity)
        {
            if (quantity > _stock)
                return BadRequest("Insufficient stock.");

            _stock -= quantity;
            await _stockHub.Clients.All.SendAsync("StockUpdated", _stock);
            return Ok(new { Stock = _stock });
        }

        [HttpGet("total")]
        public IActionResult GetTotalStock()
        {
            return Ok(new { Stock = _stock });
        }
    }
}
