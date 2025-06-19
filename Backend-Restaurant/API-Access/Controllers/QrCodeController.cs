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
            //string frontendUrl = "https://willemfrontend-fhfbeaewa5h0e3fw.germanywestcentral-01.azurewebsites.net"; Link Ruben host
            string frontendUrl = "https://willemfront-fbhjfrhydggqcfcy.westeurope-01.azurewebsites.net"; // Link Rick host
            string qrContent = $"{frontendUrl}/table/{tableId}";

            byte[] qrCodeImage = _qrCodeService.GenerateQrCode(qrContent);
            return File(qrCodeImage, "image/png");
        }
    }
}
