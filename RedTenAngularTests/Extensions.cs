using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedTenAngularTests
{
    internal static class Extensions
    {
        internal static string ToJson(this object obj)
        {
            if (obj == null) return null;
            return JsonConvert.SerializeObject(obj);
        }

        internal static T To<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
