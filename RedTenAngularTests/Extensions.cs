using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
