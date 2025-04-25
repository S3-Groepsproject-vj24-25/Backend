using Business;

namespace Test
{
    public class QrCodeServiceTests
    {
            [Fact]
            public void GenerateQrCode_ReturnsNonEmptyByteArray()
            {
                // Arrange
                var service = new QrCodeService();
                var content = "https://example.com/api/tables/scan/123";

                // Act
                var result = service.GenerateQrCode(content);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Length > 0);
            }

            [Fact]
            public void GenerateQrCode_DifferentContent_CreatesDifferentQrCodes()
            {
                // Arrange
                var service = new QrCodeService();
                var content1 = "https://example.com/api/tables/scan/123";
                var content2 = "https://example.com/api/tables/scan/456";

                // Act
                var result1 = service.GenerateQrCode(content1);
                var result2 = service.GenerateQrCode(content2);

                // Assert
                Assert.NotEqual(result1, result2);
            }

            [Fact]
            public void GenerateQrCode_SameContent_CreatesSameQrCode()
            {
                // Arrange
                var service = new QrCodeService();
                var content = "https://example.com/api/tables/scan/123";

                // Act
                var result1 = service.GenerateQrCode(content);
                var result2 = service.GenerateQrCode(content);

                // Assert
                Assert.Equal(result1, result2);
            }

            [Fact]
            public void GenerateQrCode_WithDifferentPixelsPerModule_CreatesQrCodesOfDifferentSizes()
            {
                // Arrange
                var service = new QrCodeService();
                var content = "https://example.com/api/tables/scan/123";

                // Act
                var result1 = service.GenerateQrCode(content, 10);
                var result2 = service.GenerateQrCode(content, 20);

                // Assert
                Assert.NotEqual(result1.Length, result2.Length);
            }
        }
    }