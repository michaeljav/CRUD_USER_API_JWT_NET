using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi2.App_Data;
using WebApi2.Controllers;
using WebApi2.Models;

namespace WebApi2.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public async Task Authenticate_ShouldReturnUserAuthenticated()
        {
            // Arrange
            var context = new TestSecurityDBContext();
            context.Users.Add(new User
            {
                Use_Id = 1,
                Use_UserName = "mjavier",
                Use_Password = "1234567",
                Use_FirstName = "Michael",
                Use_LastName = "Javier Mota",
                Use_Phone = "8298657498",
                Use_AddressOfStreet = "Address",
                Use_City = "City",
                Use_State = "State",
                Use_Zip = "Zip Code",
                Use_IsActive = true,
                Use_CreateDate = DateTime.Now,
                Use_VersionDate = DateTime.Now
            });

            context.Users.Add(new User
            {
                Use_Id = 2,
                Use_UserName = "rcalmona",
                Use_Password = "1234567",
                Use_FirstName = "Ramón",
                Use_LastName = "Calmona",
                Use_Phone = "8498657494",
                Use_AddressOfStreet = "Address",
                Use_City = "City",
                Use_State = "State",
                Use_Zip = "Zip Code",
                Use_IsActive = true,
                Use_CreateDate = DateTime.Now,
                Use_VersionDate = DateTime.Now
            });
            context.Users.Add(new User
            {
                Use_Id = 3,
                Use_UserName = "speralta",
                Use_Password = "1234567",
                Use_FirstName = "Santos",
                Use_LastName = "Peralta",
                Use_Phone = "8098657493",
                Use_AddressOfStreet = "Address",
                Use_City = "City",
                Use_State = "State",
                Use_Zip = "Zip Code",
                Use_IsActive = true,
                Use_CreateDate = DateTime.Now,
                Use_VersionDate = DateTime.Now
            });
            

            

         // Act
         var controller = new LoginController(context);
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Username = "mjavier";
            loginRequest.Password = "1234567";

            
            IQueryable<User> result = await controller.GetUsersAsync(loginRequest) ;
           
            // Assert            
            Assert.AreEqual(1, result.Count());

        }
    }
}
