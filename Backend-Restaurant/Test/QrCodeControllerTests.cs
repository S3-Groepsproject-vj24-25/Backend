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

            var controller = new QrCodeController(qrCodeService, mockConfiguration.Object);

            // Setup for the controller
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

            // Verify the URL
            var expectedQrContent = $"https://willemfrontend-fhfbeaewa5h0e3fw.germanywestcentral-01.azurewebsites.net/table/{tableId}";
            Assert.Equal(expectedQrContent, qrCodeService.LastCalledContent);
        }

        [Fact]
        public void GetTableQrCode_ThrowsException_WhenServiceFails()
        {
            // Arrange
            var qrCodeService = new TestQrCodeService(shouldThrow: true);
            var mockConfiguration = new Mock<IConfiguration>();
            var tableId = 123;

            var controller = new QrCodeController(qrCodeService, mockConfiguration.Object);

            // Setup for the controller
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
