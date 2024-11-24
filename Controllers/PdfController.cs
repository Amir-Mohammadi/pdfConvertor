using amandaReport.Core.Data.Domain;
using amandaReport.Services;
using Microsoft.AspNetCore.Mvc;

namespace amandaReport.Controllers
{
    public class PdfController : ControllerBase
    {
        private readonly PdfGenerationService _pdfGenerationService;

        public PdfController(PdfGenerationService pdfGenerationService)
        {
            _pdfGenerationService = pdfGenerationService;
        }


//        [HttpGet("generate-report")]
//        public async Task<IActionResult> GenerateReport()
//        {
//            // Get data from database
//            var events = new List<EventDto>
//{
//    new EventDto
//    {
//        id = 1,
//        object_type = "Sensor",
//        zone_id = 101,
//        unit_id = 12,
//        ip = "192.168.1.10",
//        image = "https://example.com/images/event1.jpg", // Replace with a valid image URL
//        datetime = new DateTime(2024, 11, 21, 10, 30, 0)
//    },
//    new EventDto
//    {
//        id = 2,
//        object_type = "Camera",
//        zone_id = 102,
//        unit_id = 14,
//        ip = "192.168.1.11",
//        image = "https://example.com/images/event2.jpg", // Replace with a valid image URL
//        datetime = new DateTime(2024, 11, 21, 11, 45, 30)
//    },
//    new EventDto
//    {
//        id = 3,
//        object_type = "Detector",
//        zone_id = 103,
//        unit_id = 16,
//        ip = "192.168.1.12",
//        image = "https://example.com/images/event3.jpg", // Replace with a valid image URL
//        datetime = new DateTime(2024, 11, 21, 12, 10, 45)
//    },
//    new EventDto
//    {
//        id = 4,
//        object_type = "Sensor",
//        zone_id = 104,
//        unit_id = 18,
//        ip = "192.168.1.13",
//        image = "https://example.com/images/event4.jpg", // Replace with a valid image URL
//        datetime = new DateTime(2024, 11, 21, 14, 20, 55)
//    },
//    new EventDto
//    {
//        id = 5,
//        object_type = "Camera",
//        zone_id = 105,
//        unit_id = 20,
//        ip = "192.168.1.14",
//        image = "https://example.com/images/event5.jpg", // Replace with a valid image URL
//        datetime = new DateTime(2024, 11, 21, 15, 35, 10)
//    }
//};
//            // Pass data to Razor view
//            var htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates" , "EventReport.cshtml");
//            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EventReport.pdf");

//            // Convert the Razor view to PDF
//            var pdfPath = await _pdfGenerationService.DataBind(htmlFilePath, outputPath, events);

//            // Ensure the PDF file exists before trying to return it
//            if (System.IO.File.Exists(pdfPath))
//            {
//                var fileBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
//                var fileName = Path.GetFileName(pdfPath);

//                // Return the file for download
//                return File(fileBytes, "application/pdf", fileName);
//            }

//            return NotFound("PDF file could not be generated.");
//        }


        [HttpGet("convert")]
        public async Task<IActionResult> ConvertToPdf()
        {
            var htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates", "EventReport.cshtml");
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EventReport.pdf");

            // Convert the HTML to PDF and save it
            var pdfPath = await _pdfGenerationService.ConvertHtmlToPdfAsync(htmlFilePath, outputPath);

            // Ensure the PDF file exists before trying to return it
            if (System.IO.File.Exists(pdfPath))
            {
                var fileBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
                var formattedDate = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                var fileName = Path.GetFileNameWithoutExtension(pdfPath) + "-" + formattedDate + Path.GetExtension(pdfPath);




                // Return the file for download
                return File(fileBytes, "application/pdf", fileName);
            }

            // Return a not found response if the PDF was not generated
            return NotFound("PDF file could not be generated.");
        }




    }
}
