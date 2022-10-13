using RefactorThisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace RefactorThisAPI.Controllers
{
    public class ProductsAPIController : ApiController
    {
        ProductsandOptionsEntities db = new ProductsandOptionsEntities();

        #region
        // GET /products - gets all products.
        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            List<Product> productsList = db.Products.ToList();
            return Ok(productsList);
        }

        //POST /products - creates new product.
        [HttpPost]
        public IHttpActionResult CreateNewProduct(Product p)
        {
            p.Id = Guid.NewGuid();
            db.Products.Add(p);
            db.SaveChanges();
            return Ok();
        }

        //GET /products?name={name} - Gets product by name.
        [HttpGet]
        public IHttpActionResult GetProductByName(string name)
        {
            var product = db.Products.Where(model => model.Name.Contains(name));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //GET /products/{id} - Gets product by id.
        [HttpGet]
        public IHttpActionResult GetProductById(Guid Id)
        {
            var product = db.Products.Where(model => model.Id == Id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //PUT /products/{id} - updates product by id.
        [HttpPut]
        public IHttpActionResult UpdateProduct(Product p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            //var product = db.Products.Where(model => model.Id == p.Id).FirstOrDefault();
            //if (product != null)
            //{
            //    product.Id = p.Id;
            //    product.Name = p.Name;
            //    product.Description = p.Description;
            //    product.Price = p.Price;
            //    product.DeliveryPrice = p.DeliveryPrice;
            //    db.SaveChanges();
            //}
            //else
            //{
            //    return NotFound();
            //}

            return Ok();
        }

        // DELETE /products/{id} - deletes product by id.
        [HttpDelete]
        public IHttpActionResult DeleteProduct(Guid Id)
        {
            var product = db.Products.Where(model => model.Id == Id).FirstOrDefault();
            db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }


        #endregion
    }
}
