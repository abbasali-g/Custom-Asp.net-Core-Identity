using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
        public static class Product
        {
            public const string View    = "Permissions.Product.View";
            public const string Create  = "Permissions.Product.Create";
            public const string Edit    = "Permissions.Product.Edit";
            public const string Delete  = "Permissions.Product.Delete";
        }
    }
}
