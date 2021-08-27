using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.Models
{
    public class CustomUser
    {
        public int Use_Id { get; set; }
        public string Use_UserName { get; set; }
        public string Use_Password { get; set; }
        public string Use_FirstName { get; set; }
        public string Use_LastName { get; set; }
        public string Use_Phone { get; set; }
        public string Use_email { get; set; }
        public string Use_AddressOfStreet { get; set; }
        public string Use_City { get; set; }
        public string Use_State { get; set; }
        public string Use_Zip { get; set; }
        public bool Use_IsActive { get; set; }
        [JsonIgnore]
        public System.DateTime Use_CreateDate { get; set; }
        [JsonIgnore]
        public Nullable<System.DateTime> Use_VersionDate { get; set; }
    }
}