using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using BitFerService.DataObjects;
using BitFerService.Models;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System;

namespace BitFerService.Controllers
{
   [Authorize]
    public class BitFerUsersController : TableController<BitFerUsers>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            BitFerContext context = new BitFerContext();
            DomainManager = new EntityDomainManager<BitFerUsers>(context, Request);
        }

      public IHttpActionResult Post([FromBody] JObject assertion)
      {
         if (isValidAssertion(assertion)) // user-defined function, checks against a database
         {
            JwtSecurityToken token = AppServiceLoginHandler.CreateToken(new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, assertion["username"]) },
                mySigningKey,
                myAppURL,
                myAppURL,
                TimeSpan.FromHours(24));
            return Ok(new LoginResult()
            {
               AuthenticationToken = token.RawData,
               User = new LoginResultUser() { UserId = userName.ToString() }
            });
         }
         else // user assertion was not valid
         {
            return Request.CreateUnauthorizedResponse();
         }
      }

      // GET tables/BitFerUsers
      public IQueryable<BitFerUsers> GetAllBitFerUsers()
        {
            return Query();
      }

        // GET tables/BitFerUsers/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<BitFerUsers> GetBitFerUsers(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/BitFerUsers/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<BitFerUsers> PatchBitFerUsers(string id, Delta<BitFerUsers> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/BitFerUsers
        public async Task<IHttpActionResult> PostBitFerUsers(BitFerUsers item)
        {
            BitFerUsers current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

      // POST tables/BitFerUsersAuthenticate
      public async Task<IHttpActionResult> BitFerUsersAuthenticate([FromBody] JObject credential)
      {
         BitFerUsers result = Query().Where(t => t.Phone == credential["Phone"].ToString()).First();
         if(result!=null)
         {
            //verify OTP
            TimeSpan span = (TimeSpan)(DateTimeOffset.UtcNow - result.UpdatedAt);
            if(span.TotalSeconds > 30)
            {
               //Send OTP
            }
            else
            {
               if (result.Otp == int.Parse(credential["Otp"].ToString()))
               {
                  //Send JWT
                  JwtSecurityToken token = new JwtSecurityToken();
               }
               else
               {
                  //error
               }
            }
         }
         else
         {
            //send OTP
         }
         BitFerUsers current = await InsertAsync(item);
         return CreatedAtRoute("Tables", new { id = current.Id }, current);
      }

      // DELETE tables/BitFerUsers/48D68C86-6EA6-4C25-AA33-223FC9A27959
      public Task DeleteBitFerUsers(string id)
        {
             return DeleteAsync(id);
        }
    }
}
