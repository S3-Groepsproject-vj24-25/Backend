using API_Access.Controllers;
using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Test
{
    public class QrCodeControllerTests
    {
        [Fact]
        public void GetTableQrCode_ReturnsFileResult_WithCorrectContentType()
        {
            // Arrange
            var fakeQrCodeBytes = new byte[] { 1, 2, 3, 4, 5 };
            var qrCodeService = new TestQrCodeService(fakeQrCodeBytes);

            var mockConfiguration = new Mock<IConfiguration>();
            var tableId = 123;

            var testUrl = "https://test.com";
            mockConfiguration.Setup(c => c["ApplicationUrl"]).Returns(testUrl);

            var controller = new QrCodeController(qrCodeService, mockConfiguration.Object);

            // HttpContext for the controller
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = controller.GetTableQrCode(tableId);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("image/png", fileResult.ContentType);
            Assert.Equal(fakeQrCodeBytes, fileResult.FileContents);

            // Verify URL
            var expectedQrContent = $"{testUrl}/WILLEM/table/{tableId}";
            Assert.Equal(expectedQrContent, qrCodeService.LastCalledContent);
        }

        [Fact]
        public void GetTableQrCode_WithoutApplicationUrl_UsesRequestSchemeAndHost()
        {
            // Arrange
            var fakeQrCodeBytes = new byte[] { 1, 2, 3, 4, 5 };
            var qrCodeService = new TestQrCodeService(fakeQrCodeBytes);

            var mockConfiguration = new Mock<IConfiguration>();
            var tableId = 123;

            // Don't set ApplicationUrl so controller falls back to Request.Scheme and Request.Host
            mockConfiguration.Setup(c => c["ApplicationUrl"]).Returns((string)null);

            // Setup HttpContext for the controller with scheme and host
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("localhost:5001");

            var controller = new QrCodeController(qrCodeService, mockConfiguration.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = controller.GetTableQrCode(tableId);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("image/png", fileResult.ContentType);
            Assert.Equal(fakeQrCodeBytes, fileResult.FileContents);

            // Verify the  URL
            var expectedQrContent = $"https://localhost:5001/WILLEM/table/{tableId}";
            Assert.Equal(expectedQrContent, qrCodeService.LastCalledContent);
        }

        [Fact]
        public void GetTableQrCode_ThrowsException_WhenServiceFails()
        {
            // Arrange
            var qrCodeService = new TestQrCodeService(shouldThrow: true);
            var mockConfiguration = new Mock<IConfiguration>();
            var tableId = 123;

            mockConfiguration.Setup(c => c["ApplicationUrl"]).Returns("https://test.com");

            var controller = new QrCodeController(qrCodeService, mockConfiguration.Object);

            // HttpContext for the controller
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => controller.GetTableQrCode(tableId));
        }
    }
}
