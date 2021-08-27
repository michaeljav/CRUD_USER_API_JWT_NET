using Currency.CustomModels;
using Currency.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi2.App_Data;
using WebApi2.Helper;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    /// <summary>
    /// login  for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private ISecurityDBEntities db = new SecurityEntities();
        public LoginController(){}
        public LoginController( ISecurityDBEntities ctx)
        {
            db = ctx;
        }
        public async Task<IQueryable<User>> GetUsersAsync(LoginRequest login)
        {
            try
            {

                var t =  db.Users.Where(x => x.Use_UserName == login.Username && x.Use_Password == login.Password).AsQueryable();
                

                return t;
               
            }
            catch (Exception ex )
            {

                throw;
            }
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IHttpActionResult> AuthenticateAsync(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                
                    //Validate credentials 
                    IQueryable<User> user = await GetUsersAsync(login);
                    //Custom User to return
                    CustomUser customUser = new CustomUser();

                    //Only one user should have
                    if (user.Count() == 1)
                    {
                        //Copy properties
                        PropertyCopier.CopyPropertiesTo(user.First(), customUser);

                        //Generate Token
                        var token = await  JwtService.GenerateToken(customUser);

                    // var t =   Ok(new Response(true, "", token));
                        return Ok(new Response(true,"",token));
                    }
                    else
                    {

                        return Content(HttpStatusCode.Unauthorized, new Response(false, "Unauthorized", new Object()));
                    }

                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new Response(false,ex.Message, new Object()));
               
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

    }
}
