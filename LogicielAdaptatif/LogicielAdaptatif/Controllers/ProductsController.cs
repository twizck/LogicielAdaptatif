using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LogicielAdaptatif.Models;

namespace LogicielAdaptatif.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly YNOVEntities db = new YNOVEntities();

        //recupère tous les produits
        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        //recupère produit spé
        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            try
            {
                //cherche dans la base un produit avec le même id
                Product product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }

        //Modification d'un produit
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != product.id_product)
                {
                    return BadRequest();
                }

                db.Entry(product).State = EntityState.Modified;

                try
                {
                    //enregistrement dans la base
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = product.id_product }, product);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            try
            {
                Product product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                db.Products.Remove(product);
                await db.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.id_product == id) > 0;
        }
    }
}