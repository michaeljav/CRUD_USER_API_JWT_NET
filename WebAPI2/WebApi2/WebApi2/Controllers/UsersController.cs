using Currency.CustomModels;
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
using WebApi2.App_Data;
using WebApi2.Helper;
using WebApi2.Models;

namespace WebApi2.Controllers
{
   
    //[AllowAnonymous]
    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        //private SecurityEntities db = new SecurityEntities();



        public async Task<List<User>> GetUserOrAllAsync(int? id=null)
        {
            try
            {
                using (SecurityEntities db = new SecurityEntities())
                {
                    if (id != null)
                    {
                        return await db.Users.Where(x => x.Use_Id == id).ToListAsync();
                    }
                    return await db.Users.ToListAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        // GET: api/User/all    
        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetUsersAsync()
        {
                     

            try
            {
                return Ok(new Response(true, "", await GetUserOrAllAsync()));
              
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.InternalServerError, new Response(false, ex.Message, new Object()));
            }
            
        }

        // GET: api/User/all
      
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetUserIdAsync(int id)
        {
            if (id < 1)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {

                //Get User
                List<User> users = await GetUserOrAllAsync(id);

                //Custom User to return
                CustomUser customUser = new CustomUser();

                //Only one user should have
                if (users.Count() == 1)
                {
                    //Copy properties
                    PropertyCopier.CopyPropertiesTo(users[0], customUser);                                       
                    
                    return Ok(new Response(true, "", customUser));
                }
                else if (users.Count() == 0)
                {

                    return Content(HttpStatusCode.Unauthorized, new Response(false, "No exist this user", new Object()));
                }
                else 
                 {

                    return Content(HttpStatusCode.Unauthorized, new Response(false, "Exists more than one record", new Object()));
                }


            }
            catch (Exception ex)
            {
                return  Content(HttpStatusCode.InternalServerError, new Response(false, ex.Message, new Object()));

            }
        }


     
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="customUser"></param>
        /// <returns></returns>
        // POST: api/Users
        //[Authorize]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(CustomUser customUser)
        {
            try
            {
                User user = new User();
                //if (customUser == null)
                //{
                //    return Content(HttpStatusCode.BadRequest, new Response(false, "Please fill field Use_IsActive = true or false ", new Object()));
                //}

              
                
                //Shouldn't be empty
                //username,password,FirstName,LastName
                if (customUser == null ||string.IsNullOrWhiteSpace(customUser.Use_UserName) ||
                    string.IsNullOrWhiteSpace(customUser.Use_Password) ||
                    string.IsNullOrWhiteSpace(customUser.Use_FirstName) ||
                    string.IsNullOrWhiteSpace(customUser.Use_FirstName) 
                    )
                {
               
                    return Content(HttpStatusCode.BadRequest, new Response(false, "Please fill fields required:Use_UserName,Use_Password,Use_LastName,Use_IsActive = true or false  ", new Object()));
                }

                //bool valueSend = false;
                //bool isBool = bool.TryParse(customUser.Use_IsActive, out valueSend);
                //if (!isBool)
                //{
                //    return Content(HttpStatusCode.BadRequest, new Response(false, "Please fill field Use_IsActive = true or false ", new Object()));
                //}


                using (SecurityEntities db = new SecurityEntities())
                {
                    //Valid if the same user exists in the database
                    var existUser = await db.Users.Where(x => x.Use_UserName == customUser.Use_UserName).SingleOrDefaultAsync();
                    if (existUser != null)
                    {
                        return Content(HttpStatusCode.BadRequest, new Response(false, "User already exist! ", new Object()));
                    }

                  
                    //Copy properties
                    PropertyCopier.CopyPropertiesTo(customUser, user);
                    //PropertyCopier.CopyPropertiesTo(customUser, user, "Use_IsActive");
                    //user.Use_IsActive = valueSend;
                    user.Use_CreateDate = DateTime.Now;
                    user.Use_VersionDate = DateTime.Now;


                    //Save User
                    db.Users.Add(user);
                    await db.SaveChangesAsync();


                }
                return Ok(new Response(true, "", user));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.InternalServerError, new Response(false, ex.Message, new Object()));
            }
        }

               
        /// <summary>
        /// Modify user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customUser"></param>
        /// <returns></returns>
        // PUT: api/Users/5      
        [Authorize]
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id,[FromBody]CustomUser customUser)
        {
            if (id < 1)
            {
                return Content(HttpStatusCode.BadRequest, new Response(false, "Insert User Id! ", new Object()));
            }
            //Shouldn't be empty
            //username,password,FirstName,LastName
            if (string.IsNullOrWhiteSpace(customUser.Use_UserName) ||
                string.IsNullOrWhiteSpace(customUser.Use_Password) ||
                string.IsNullOrWhiteSpace(customUser.Use_FirstName) ||
                string.IsNullOrWhiteSpace(customUser.Use_LastName)
                )
            {

                return Content(HttpStatusCode.BadRequest, new Response(false, "Please fill fields required:Use_UserName,Use_Password,Use_LastName ", new Object()));
            }
            var userToUpdate = new User();
            using (SecurityEntities db = new SecurityEntities())
            {

                //Search User to Update
                 userToUpdate = await db.Users.Where(x => x.Use_Id == id).SingleOrDefaultAsync();
                if (userToUpdate == null)
                {
                    return Content(HttpStatusCode.BadRequest, new Response(false, "User does not exist! ", new Object()));
                }

                userToUpdate.Use_UserName = customUser.Use_UserName;
                userToUpdate.Use_Password = customUser.Use_Password;
                userToUpdate.Use_FirstName = customUser.Use_FirstName;
                userToUpdate.Use_LastName = customUser.Use_LastName;
                userToUpdate.Use_Phone = customUser.Use_Phone;
                userToUpdate.Use_email = customUser.Use_email;
                userToUpdate.Use_AddressOfStreet = customUser.Use_AddressOfStreet;
                userToUpdate.Use_City = customUser.Use_City;
                userToUpdate.Use_State = customUser.Use_State;
                userToUpdate.Use_Zip = customUser.Use_Zip;
                userToUpdate.Use_VersionDate = DateTime.Now;


                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Content(HttpStatusCode.InternalServerError, new Response(false, ex.Message, new Object()));
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, new Response(false, ex.Message, new Object()));
                }
            }
               return Ok(new Response(true, "", userToUpdate));
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {

            if (id < 1)
            {
                return Content(HttpStatusCode.BadRequest, new Response(false, "Insert User Id! ", new Object()));
            }

            User user = new User();
            using (SecurityEntities db = new SecurityEntities())
            {

                 user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return Content(HttpStatusCode.NotFound, new Response(false, "Not Found! ", new Object()));
                }

                db.Users.Remove(user);
                await db.SaveChangesAsync();

            }
            return Ok(new Response(true, "User removed", user));
        }

      
    }
}