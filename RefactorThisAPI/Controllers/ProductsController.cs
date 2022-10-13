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
    public class ProductsController : Controller
    {

        HttpClient client = new HttpClient();

        #region
        // GET /products - gets all products.
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            List<Product> productsList = new List<Product>();
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.GetAsync("ProductsAPI");
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<List<Product>>();
                json.Wait();
                productsList = json.Result;
            }
            return View(productsList);
        }


        public ActionResult CreateNewProduct()
        {
            return View();
        }

        //POST /products - creates new product.
        [HttpPost]
        public ActionResult CreateNewProduct(Product p)
        {
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.PostAsJsonAsync<Product>("ProductsAPI", p);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return View("CreateNewProduct");
        }

        //GET /products?name={name} - Gets product by name.
        [HttpGet]
        public ActionResult GetProductByName(string name)
        {
            List<Product> productsList = new List<Product>();
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.GetAsync("ProductsAPI?name=" + name.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<List<Product>>();
                json.Wait();
                productsList = json.Result;
            }
            return View(productsList);
        }

        //GET /products/{id} - Gets product by id.
        [HttpGet]
        public ActionResult GetProductById(Guid Id)
        {
            Product p = null;
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.GetAsync("ProductsAPI?id=" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<Product>();
                json.Wait();
                p = json.Result;
            }
            return View(p);
        }

        public ActionResult UpdateProduct(Guid Id)
        {
            Product p = null;
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.GetAsync("ProductsAPI?id=" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<Product>();
                json.Wait();
                p = json.Result;
            }
            return View(p);
        }

        //PUT /products/{id} - updates product by id.
        [HttpPost]
        public ActionResult UpdateProduct(Product p)
        {
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.PutAsJsonAsync<Product>("ProductsAPI", p);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return View("UpdateProduct");
        }

        public ActionResult DeleteProduct(Guid Id)
        {
            Product p = null;
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.GetAsync("ProductsAPI?id=" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsAsync<Product>();
                json.Wait();
                p = json.Result;
            }
            return View(p);
        }


        //DELETE /products/{id} - deletes product by id.
        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult DeleteProducts(Guid Id)
        {
            client.BaseAddress = new Uri("https://localhost:44395/api/ProductsAPI");
            var response = client.DeleteAsync("ProductsAPI/" + Id.ToString());
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return View("DeleteProduct");
        }


        #endregion

    }
}