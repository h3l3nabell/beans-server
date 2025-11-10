using Microsoft.AspNetCore.SignalR;
namespace beans_server
{
    public class StockHub : Hub
    {
        public async Task NotifyStockChanged(int newStockLevel)
        {
            await Clients.All.SendAsync("StockUpdated", newStockLevel);
        }
    }
}
