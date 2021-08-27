namespace WebApi2.Tests
{
 
    using System;
    using System.Data.Entity;
    using WebApi2.App_Data;

    public class TestSecurityDBContext : ISecurityDBEntities
    {
        public TestSecurityDBContext()
        {
          
           // this.Users = new TestUserDbSet();
            this.Users = new TestUserDbSet();
        }

        

        public DbSet<User> Users { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
        
        public void Dispose() { }

        

     

        public int proc_SaveUsers(int? id, string Use_UserName, string Use_Password, string Use_FirstName,
            string Use_LastName, string Use_Phone, string Use_email, string Use_AddressOfStreet, string Use_City, string Use_State,
            string Use_Zip, bool Use_IsActive, System.DateTime Use_CreateDate, Nullable<System.DateTime> Use_VersionDate)
        {
            return 1;
        }
    }
}
