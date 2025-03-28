using Business;
using Microsoft.AspNetCore.Mvc;

namespace API_Access.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : Controller
    {
        private readonly QrCodeService _qrCodeService;
        private readonly IConfiguration _configuration;

        public QrCodeController(QrCodeService qrCodeService, IConfiguration configuration)
        {
            _qrCodeService = qrCodeService;
            _configuration = configuration;
        }

        [HttpGet("table/{tableId}")]
        public IActionResult GetTableQrCode(int tableId)
        {
            string baseUrl = _configuration["ApplicationUrl"] ?? $"{Request.Scheme}://{Request.Host}";
            string qrContent = $"{baseUrl}/api/tables/scan/{tableId}";

            byte[] qrCodeImage = _qrCodeService.GenerateQrCode(qrContent);
            return File(qrCodeImage, "image/png");
        }
    }
}
