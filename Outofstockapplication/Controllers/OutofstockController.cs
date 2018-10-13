using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OutofstockDataAccess;

namespace Outofstockapplication.Controllers
{
    
    public class OutofstockController : ApiController
    {
        public IEnumerable<Product> Get()
        {
           
            using (outofstockEntities entities = new outofstockEntities())
            {
                var entity = entities.Products.Count();

               
                   return entities.Products.ToList();
                
            }
        }

        public HttpResponseMessage   Get(int id)
        {
            using (outofstockEntities entities = new outofstockEntities())
            {
                var entity= entities.Products.FirstOrDefault(e=> e.Id==id);
                if(entity!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product Not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Product product)
        {
            try
            {
                using (outofstockEntities entities = new outofstockEntities())
                {
                    entities.Products.Add(product);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.Id.ToString());
                    return message;

                }
            }
            catch(Exception ex)
            {
               return  Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (outofstockEntities entities = new outofstockEntities())
                {
                    var entity = entities.Products.FirstOrDefault(e => e.Id == id);

                    if (entity != null)
                    {
                        entities.Products.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product Not found");
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }


       

    }
}
