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
    public class ProductOptionsAPIController : ApiController
    {
        ProductsandOptionsEntities db = new ProductsandOptionsEntities();

        #region
        // GET /products/options - gets all options.
        [HttpGet]
        public IHttpActionResult GetAllProductOptions()
        {
            List<ProductOption> productoptionList = db.ProductOptions.ToList();
            return Ok(productoptionList);
        }

        //POST /products/{id}/options - creates new product option to the specified product.
        [HttpPost]
        public IHttpActionResult CreateNewProductOption(ProductOption p)
        {
            p.Id = Guid.NewGuid();
            db.ProductOptions.Add(p);
            db.SaveChanges();
            return Ok();
        }


        //GET /products/{id}/options/{optionId} - finds the specified product option for the specified Id.
        [HttpGet]
        public IHttpActionResult GetProductOptionById(Guid Id)
        {
            var productoption = db.ProductOptions.Where(model => model.Id == Id).FirstOrDefault();
            if (productoption == null)
            {
                return NotFound();
            }
            return Ok(productoption);
        }

        //PUT /products/{id}/options/{optionId} - updates the specified product option.
        [HttpPut]
        public IHttpActionResult UpdateProductOption(ProductOption p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        // DELETE /products/{id}/options/{optionId} - deletes the specified product option.
        [HttpDelete]
        public IHttpActionResult DeleteProductOption(Guid Id)
        {
            var productoption = db.ProductOptions.Where(model => model.Id == Id).FirstOrDefault();
            db.Entry(productoption).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }

        #endregion


    }
}
