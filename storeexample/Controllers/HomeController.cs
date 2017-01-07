using Geocoding.Google;
using storeexample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers
{
    public class HomeController : BaseController
    {
        private StoreExampleModel db = new StoreExampleModel();

        public ActionResult Index(HomePageViewModel model)
        {
            var store = db.Store.FirstOrDefault();
            if (store != null && string.IsNullOrEmpty(store.HomeSplashImageUrl) == false)
            {
                ViewBag.HomeSplashImageUrl = store.HomeSplashImageUrl;
            }

            model.HomePageCrumbs = db.HomePageCrumbs.ToList();

            // if our store delivers we'll show the zip code form
            if (store.DeliveryAvailable)
            {
                model.ShowZipCodeForm = true;
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(ContactViewModel model)
        {
            model.Contacts = db.Contacts.ToList();
            ViewBag.Message = "Your contact page.";

            return View(model);
        }

        [HttpPost]
        public ActionResult VerifyZipCode(string zipCode)
        {
            string city = null;
            string state = null;

            var request = Request;

            int parsedZip;

            //check if zipcode is a valid integer
            if (int.TryParse(zipCode, out parsedZip) == false)
            {
                return new JsonResult() { Data = new { Success = false, Message = "Invalid ZipCode" } };
            }

            //get city and state from geocode API
            var geoCoder = new GoogleGeocoder()
            {
                ComponentFilters = new List<GoogleComponentFilter> { new GoogleComponentFilter("country", "US"), new GoogleComponentFilter("postal_code", zipCode) }
            };

            try
            {
                GoogleAddress address = geoCoder.Geocode(zipCode).FirstOrDefault();

                if(address == null)
                {
                    return new JsonResult() { Data = new { Success = false, Message = "Invalid ZipCode" } };
                }

                city = address.Components.Single(x => x.Types.Any(t => t == GoogleAddressType.Locality)).ShortName;
                state = address.Components.Single(x => x.Types.Any(t => t == GoogleAddressType.AdministrativeAreaLevel1)).ShortName;

            }
            catch
            {
                return new JsonResult() { Data = new { Success = false, Message = "Invalid ZipCode" } };
            }

            //check if zip is serviced
            var zipcode = db.ZipCodes.SingleOrDefault(z => z.Zip == parsedZip);
            
            if (zipcode == null)
            {
                db.ZipCodes.Add(new ZipCode() { Zip = parsedZip, IsServiced = false });
                db.SaveChanges();
                return new JsonResult() { Data = new { Success = false, Message = db.Store.First().ZipCodeNotInServiceMessage } };
            }
            else if (zipcode.IsServiced)
            {
                var order = db.Orders.Add(new Order()
                {
                    ZipCode = zipcode,
                    DateOrdered = DateTime.Now,
                    Status = OrderStatus.Initial,
                    City = city,
                    State = state,
                });

                db.SaveChanges();

                return new JsonResult() { Data = new { Success = true, Message = "Let's Order!", OrderId = order.OrderId } };
            }
            else
            {
                return new JsonResult() { Data = new { Success = false, Message = db.Store.First().ZipCodeNotInServiceMessage } };
            }

        }
    }
}