using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using BitFerService.DataObjects;
using BitFerService.Models;

namespace BitFerService.Controllers
{
   [Authorize]
    public class BitFerTransactionsController : TableController<BitFerTransactions>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            BitFerContext context = new BitFerContext();
            DomainManager = new EntityDomainManager<BitFerTransactions>(context, Request);
        }

        // GET tables/BitFerTransactions
        public IQueryable<BitFerTransactions> GetAllBitFerTransactions()
        {
            return Query(); 
        }

        // GET tables/BitFerTransactions/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<BitFerTransactions> GetBitFerTransactions(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/BitFerTransactions/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<BitFerTransactions> PatchBitFerTransactions(string id, Delta<BitFerTransactions> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/BitFerTransactions
        public async Task<IHttpActionResult> PostBitFerTransactions(BitFerTransactions item)
        {
            BitFerTransactions current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/BitFerTransactions/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBitFerTransactions(string id)
        {
             return DeleteAsync(id);
        }
    }
}
