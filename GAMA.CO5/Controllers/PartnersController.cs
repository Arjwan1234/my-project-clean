using Microsoft.AspNetCore.Mvc;
using GAMA.CO5.Models;

namespace GAMA_ASP_MVC_CLEAN.Controllers
{
    public class PartnersController : Controller
    {
        public IActionResult Index()
        {
            var model = new PartnersViewModel
            {
                PageTitle = "شركاؤنا",
                Partners = GetAllPartners()
            };

            return View(model);
        }

        private List<Partner> GetAllPartners()
        {
            return new List<Partner>
            {
                new Partner
                {
                    Id = "stc",
                    Name = "STC",
                    LogoUrl = "https://via.placeholder.com/150x80/2596be/fff?text=STC",
                    WebsiteUrl = "https://stc.com.sa"
                },
                new Partner
                {
                    Id = "elm",
                    Name = "Elm",
                    LogoUrl = "https://via.placeholder.com/150x80/2596be/fff?text=ELM",
                    WebsiteUrl = "https://elm.sa"
                },
                new Partner
                {
                    Id = "microsoft",
                    Name = "Microsoft",
                    LogoUrl = "https://via.placeholder.com/150x80/2596be/fff?text=Microsoft",
                    WebsiteUrl = "https://microsoft.com"
                },
                new Partner
                {
                    Id = "aws",
                    Name = "Amazon AWS",
                    LogoUrl = "https://via.placeholder.com/150x80/2596be/fff?text=AWS",
                    WebsiteUrl = "https://aws.amazon.com"
                },
                new Partner
                {
                    Id = "oracle",
                    Name = "Oracle",
                    LogoUrl = "https://via.placeholder.com/150x80/2596be/fff?text=Oracle",
                    WebsiteUrl = "https://oracle.com"
                },
                new Partner
                {
                    Id = "sap",
                    Name = "SAP",
                    LogoUrl = "https://via.placeholder.com/150x80/2596be/fff?text=SAP",
                    WebsiteUrl = "https://sap.com"
                }
            };
        }
    }
}
