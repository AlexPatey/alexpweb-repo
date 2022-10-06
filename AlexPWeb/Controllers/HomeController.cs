using AlexPWeb.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AlexPWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult ConvertImgToText(string imgSrc)
        {
            imgSrc = imgSrc.Replace("data:image/jpeg;base64,", "");
            imgSrc = imgSrc.Replace("data:image/png;base64,", "");

            byte[] bytes = Convert.FromBase64String(imgSrc);

            Google.Cloud.Vision.V1.Image image1 = Google.Cloud.Vision.V1.Image.FromBytes(bytes);

            ImageAnnotatorClientBuilder builder = new ImageAnnotatorClientBuilder
            {
                CredentialsPath = "D:\\Working Folder\\AlexPWeb\\AlexPWeb\\GoogleAPIKey\\alexpweb-4891e60e4e6a.json"
            };

            ImageAnnotatorClient client = builder.Build();

            TextAnnotation text = client.DetectDocumentText(image1);
            Console.WriteLine($"Text: {text.Text}");
            foreach (var page in text.Pages)
            {
                foreach (var block in page.Blocks)
                {
                    string box = string.Join(" - ", block.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                    Console.WriteLine($"Block {block.BlockType} at {box}");
                    foreach (var paragraph in block.Paragraphs)
                    {
                        box = string.Join(" - ", paragraph.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                        Console.WriteLine($"  Paragraph at {box}");
                        foreach (var word in paragraph.Words)
                        {
                            Console.WriteLine($"    Word: {string.Join("", word.Symbols.Select(s => s.Text))}");
                        }
                    }
                }
            }

            return Json(new {success = true, text = text.Text});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}