namespace WebApi2.App_Data
{
    using System;
    using System.Data.Entity;
    

    public interface ISecurityDBEntities: IDisposable
    {
        DbSet<User> Users { get; }
       

        int SaveChanges();

        
        int proc_SaveUsers(Nullable<int> Use_Id, string Use_UserName, string Use_Password, string Use_FirstName,
            string Use_LastName, string Use_Phone, string Use_email, string Use_AddressOfStreet, string Use_City, string Use_State,
            string Use_Zip, bool Use_IsActive, System.DateTime Use_CreateDate, Nullable<System.DateTime> Use_VersionDate);

        
    }
}