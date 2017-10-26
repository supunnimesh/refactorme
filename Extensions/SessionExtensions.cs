using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace refactorme.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session,
                       string key, object value)
        {
            string stringValue = JsonConvert.SerializeObject(value);
            session.SetString(key, stringValue);
        }

        public static T GetObject<T>(this ISession session,
                                     string key)
        {
            string stringValue = session.GetString(key);
            if (!string.IsNullOrEmpty(stringValue))
            {
                T value = JsonConvert.DeserializeObject<T>(stringValue);
                return value;
            }

            return default(T);
        }
    }
}
