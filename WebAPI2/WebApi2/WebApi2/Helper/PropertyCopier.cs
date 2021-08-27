using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApi2.Helper
{
    
    public  static class PropertyCopier
    { 

        public static void CopyPropertiesTo(this object fromObject, object toObject,string fieldNotWork = null )
        {
            PropertyInfo[] toObjectProperties = toObject.GetType().GetProperties();
            foreach (PropertyInfo propTo in toObjectProperties)
            {
                PropertyInfo propFrom = fromObject.GetType().GetProperty(propTo.Name);
                if (!string.IsNullOrWhiteSpace(fieldNotWork))
                {
                    if (propFrom != null && propFrom.CanWrite && propFrom.Name != fieldNotWork)
                        propTo.SetValue(toObject, propFrom.GetValue(fromObject, null), null);
                }
                else { 
                if (propFrom != null && propFrom.CanWrite )
                    propTo.SetValue(toObject, propFrom.GetValue(fromObject, null), null);
                }
            }
        }
    }
}