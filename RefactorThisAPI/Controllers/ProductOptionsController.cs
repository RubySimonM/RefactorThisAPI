using Antlr.Runtime.Misc;
using RefactorThisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace RefactorThisAPI.Controllers
{
    public class ProductOptionsController : Controller
    {

        HttpClient client = new HttpClient();

        #region
        // GET /products/{id}/options - gets all options for a specified product.
        [HttpGet]
        public ActionResult GetAllProductOptions()
        {
            List<ProductOption> productoptionList = new List<ProductOption>();
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductOptionsAPI");
            var response = client.GetAsync("ProductOptionsAPI");
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<List<ProductOption>>();
                json.Wait();
                productoptionList = json.Result;
            }
            return View(productoptionList);
        }


        public ActionResult CreateNewProductOption()
        {
            return View();
        }

        //POST /products/{id}/options - creates new product option to the specified product.
        [HttpPost]
        public ActionResult CreateNewProductOption(ProductOption p)
        {
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductOptionsAPI");
            var response = client.PostAsJsonAsync<ProductOption>("ProductOptionsAPI", p);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProductOptions");
            }
            return View("CreateNewProductOption");
        }

        //GET /products/{id}/options/{optionId} - finds the specified product option for the specified Id.
        [HttpGet]
        public ActionResult GetProductOptionById(Guid Id)
        {
            ProductOption p = null;
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductOptionsAPI");
            var response = client.GetAsync("ProductOptionsAPI?id=" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<ProductOption>();
                json.Wait();
                p = json.Result;
            }
            return View(p);
        }

        public ActionResult UpdateProductOption(Guid Id)
        {
            ProductOption p = null;
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductOptionsAPI");
            var response = client.GetAsync("ProductOptionsAPI?id=" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<ProductOption>();
                json.Wait();
                p = json.Result;
            }
            return View(p);
        }

        //PUT /products/{id}/options/{optionId} - updates the specified product option.
        [HttpPost]
        public ActionResult UpdateProductOption(ProductOption p)
        {
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductOptionsAPI");
            var response = client.PutAsJsonAsync<ProductOption>("ProductOptionsAPI", p);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProductOptions");
            }
            return View("UpdateProductOption");
        }

        public ActionResult DeleteProductOption(Guid Id)
        {
            ProductOption p = null;
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductOptionsAPI");
            var response = client.GetAsync("ProductOptionsAPI?id=" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<ProductOption>();
                json.Wait();
                p = json.Result;
            }
            return View(p);
        }


        // DELETE /products/{id}/options/{optionId} - deletes the specified product option.
        [HttpPost, ActionName("DeleteProductOption")]
        public ActionResult DeleteProductOptions(Guid Id)
        {
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductOptionsAPI");
            var response = client.DeleteAsync("ProductOptionsAPI/" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProductOptions");
            }
            return View("DeleteProductOption");
        }

        #endregion

    }
}