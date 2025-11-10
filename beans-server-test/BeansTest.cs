using beans_server;
using beans_server.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace beans_server_test
{
    public class BeansTest
    {
        private (Mock<IHubContext<StockHub>>, Mock<IClientProxy>) CreateMockHubContext()
        {
            var mockHubContext = new Mock<IHubContext<StockHub>>();
            var mockClients = new Mock<IHubClients>();
            var mockClientProxy = new Mock<IClientProxy>();

            mockHubContext.Setup(x => x.Clients).Returns(mockClients.Object);
            mockClients.Setup(x => x.All).Returns(mockClientProxy.Object);
            mockClientProxy.Setup(x => x.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>(), default))
                          .Returns(Task.CompletedTask);

            return (mockHubContext, mockClientProxy);
        }

        [Fact]
        public async Task AddStock_IncreasesStock()
        {
            // Arrange
            var (mockHubContext, mockClientProxy) = CreateMockHubContext();
            var controller = new StockController(mockHubContext.Object);
            controller.ResetStock();

            // Act
            var result = await controller.AddStock(5);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var stockResponse = Assert.IsType<StockResponse>(okResult.Value);
            Assert.Equal(5, stockResponse.Stock);

            // Verify the hub method was called with the correct stock value
            mockClientProxy.Verify(x => x.SendCoreAsync("StockUpdated", It.Is<object[]>(args => args[0].Equals(5)), default), Times.Once);
        }


        [Fact]
        public async Task PurchaseStock_DecreasesStock()
        {
            // Arrange
            var (mockHubContext, mockClientProxy) = CreateMockHubContext();
            var controller = new StockController(mockHubContext.Object);
            controller.ResetStock();

            // First add stock to ensure we have inventory
            await controller.AddStock(10);

            // Act
            var result = await controller.PurchaseStock(5);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var stockResponse = Assert.IsType<StockResponse>(okResult.Value);
            Assert.Equal(5, stockResponse.Stock);

            // Verify the hub method was called with the remaining stock (5)
            mockClientProxy.Verify(x => x.SendCoreAsync("StockUpdated", It.Is<object[]>(args => args[0].Equals(5)), default), Times.AtLeastOnce);
        }

        [Fact]
        public async Task GetTotalStock_ReturnsStock()
        {
            // Arrange
            var (mockHubContext, mockClientProxy) = CreateMockHubContext();
            var controller = new StockController(mockHubContext.Object);
            controller.ResetStock();

            // First add stock to ensure we have inventory
            await controller.AddStock(10);

            // Act
            var result = controller.GetTotalStock();

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var stockResponse = Assert.IsType<StockResponse>(okResult.Value);
            Assert.Equal(10, stockResponse.Stock);

            // Note: GetTotalStock doesn't call SignalR, so no verification needed
        }
    }
}
